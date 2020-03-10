using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using DataAccessComponent;
using System.Configuration;
using System.Diagnostics;
using DataManipulationComponents.Constants;

namespace DataManipulationComponents.ExtensionClasses
{
    public static class DataTableExtensions
    {

        private static int precisionValue
        {
            get
            {
                return Int32.Parse(ConfigurationManager.AppSettings["DecimalPrecisionIndex"]);
            }
        }

        private static int precisionTolerence
        {
            get
            {
                return Int32.Parse(ConfigurationManager.AppSettings["PrecisionTolerencePercentage"]);
            }
        }

        private static int comaprisonLevel
        {
            get
            {
                return Int32.Parse(ConfigurationManager.AppSettings["ComparisonLevel"]);
            }
        }
        /// <summary>
        /// Fills a DataTable object with specified query's result.
        /// </summary>
        /// <param name="d">Extension Method Type declaration.</param>
        /// <param name="sql">Query to fetch Data.</param>
        public static void Fill(this DataTable d, string sql)
        {

            DataAccessLink link = new DataAccessLink();
            d.Clear();
            d.Merge(link.GetDataTable(sql));
        }

        /// <summary>
        /// Fills a DataTable object with specified query's result,
        /// fteches data from specified Database instance.
        /// </summary>
        /// <param name="d">Extension Method Type declaration.</param>
        /// <param name="sql">Query to fetch Data.</param>
        /// <param name="instanceName">Database instance Name.</param>
        public static void Fill(this DataTable d, string sql, string instanceName)
        {
            Trace.WriteLine("Entered Fill Method, Query= " + sql + "DB= " + instanceName);
            DataAccessLink link = new DataAccessLink();
            d.Clear();
            d.Merge(link.GetDataTable(sql, instanceName));
        }

        /// <summary>
        /// Compares two data Tables.
        /// Comaprison stops after first difference is found.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="dataTable"></param>
        /// <returns>A boolean value to indicate result of comparision.</returns>
        public static bool IsEqual(this DataTable d, DataTable dataTable)
        {
            if (AreBasicChecksPassed(d, dataTable))
            {
                for (int i = 0; i < d.Rows.Count; i++)
                {
                    for (int j = 0; j < d.Columns.Count; j++)
                    {
                        if (!Compare(d, dataTable, i, j))
                        {
                            return false;
                        }

                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Compares the dataTable with result of given query.
        /// </summary>
        /// <param name="d">Extension method type declaration./</param>
        /// <param name="query">Query whose result has to be compared with DataTable.</param>
        /// <returns>Returns a boolean value to indicate result of comparison.</returns>
        public static bool IsEqual(this DataTable d, string query)
        {
            DataAccessLink link = new DataAccessLink();
            return IsEqual(d, link.GetDataTable(query));

        }

        /// <summary>
        /// Compares the dataTable with result of given query.
        /// </summary>
        /// <param name="d">Extension method type declaration./</param>
        /// <param name="query">Query whose result has to be compared with DataTable.</param>
        /// <param name="dbName">Specifies the database instance to be used for executing query..</param>
        /// <returns>Returns a boolean value to indicate result of comparison.</returns>
        public static bool IsEqual(this DataTable d, string query, string dbName)
        {
            DataAccessLink link = new DataAccessLink();
            return IsEqual(d, link.GetDataTable(query, dbName));
        }

        /// <summary>
        /// Compare two DataTables.
        /// After finiding the first difference in a row rest of the row is skipped.
        /// </summary>
        /// <param name="table">Extension method type declaration.</param>
        /// <param name="dataTable">DatTable to be compared.</param>
        /// <returns>A List whose each element is a pair of int and string.
        /// Int denotes the row index.
        /// String contains the error message.
        /// </returns>
        public static List<Tuple<int, string>> Compare(this DataTable table, DataTable dataTable)
        {
            List<Tuple<int, string>> difference = new List<Tuple<int, string>>();

            if (table.Rows.Count != dataTable.Rows.Count)
            {
                difference.Add(new Tuple<int, string>(0, "Row count mismatch in Table: " + table.TableName));
            }

            else if (table.Columns.Count != dataTable.Columns.Count)
            {
                difference.Add(new Tuple<int, string>(0, "Column count mismatch in Table: " + table.TableName));
            }
            else
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (!Compare(table, dataTable, i, j))
                        {
                            difference.Add(new Tuple<int, string>(i, ErrorMessages.WriteComparisonError(table, dataTable, i, j)));
                            break;
                        }
                    }
                }
            }
            return difference;
        }

        /// <summary>
        /// Compare based on setting provided in configuration file.
        /// '0' represents  normal compare.
        /// '1' represents  deep compare.
        /// </summary>
        /// <param name="d">Extension type identifier</param>
        /// <param name="query">Result of this query is compared with the other table.</param>
        /// <returns></returns>
        public static List<Tuple<int, string>> CompareDynamic(this DataTable d, string query)
        {
            if (comaprisonLevel == 0)
            {
                return Compare(d, query);
            }
            else
            {
                return CompareDeep(d, query);
            }
        }

        /// <summary>
        /// Compare based on setting provided in configuration file.
        /// '0' represents  normal compare.
        /// '1' represents  deep compare.
        /// </summary>
        /// <param name="d">Extension type identifier</param>
        /// <param name="query">Result of this query is compared with the other table.</param>
        /// <returns></returns>
        public static List<Tuple<int, string>> CompareDynamic(this DataTable d, string query, string instanceName)
        {
            if (comaprisonLevel == 0)
            {
                return Compare(d, query, instanceName);
            }
            else
            {
                return CompareDeep(d, query, instanceName);
            }
        }

        /// <summary>
        /// Compare based on setting provided in configuration file.
        /// '0' represents  normal compare.
        /// '1' represents  deep compare.
        /// </summary>
        /// <param name="d">Extension type identifier</param>
        /// <param name="query">Result of this query is compared with the other table.</param>
        /// <returns></returns>
        public static List<Tuple<int, string>> CompareDynamic(this DataTable d, DataTable dt)
        {
            if (comaprisonLevel == 0)
            {
                return Compare(d, dt);
            }
            else
            {
                return CompareDeep(d, dt);
            }
        }

        /// <summary>
        /// Compare a Data Table with results of a query.
        /// After finiding the first difference in a row rest of the row is skipped.
        /// </summary>
        /// <param name="table">Extension method type declaration.</param>
        /// <param name="query">query whose result is to be compared.</param>
        /// <returns>
        /// A List whose each element is a pair of int and string.
        /// Int denotes the row index.
        /// String contains the error message.
        /// </returns>

        public static List<Tuple<int, string>> Compare(this DataTable d, string query)
        {
            DataAccessLink link = new DataAccessLink();
            return Compare(d, link.GetDataTable(query));
        }

        /// <summary>
        /// Compare a Data Table with results of a query.
        /// After finiding the first difference in a row rest of the row is skipped.
        /// </summary>
        /// <param name="table">Extension method type declaration.</param>
        /// <param name="query">query whose result is to be compared.</param>
        /// <param name="dbName">Name of database instance on which query will be executed.</param>
        /// <returns>
        /// A List whose each element is a pair of int and string.
        /// Int denotes the row index.
        /// String contains the error message.
        /// </returns>
        public static List<Tuple<int, string>> Compare(this DataTable d, string query, string dbName)
        {
            DataAccessLink link = new DataAccessLink();
            return Compare(d, link.GetDataTable(query, dbName));
        }

        /// <summary>
        /// Comapres all values of two Data Tables.
        /// </summary>
        /// <param name="table">Extension method type declaration.</param>
        /// <param name="dataTable">DataTable to compared.</param>
        /// <returns>
        /// A List whose each element is a pair of int and string.
        /// Int denotes the row index.
        /// String contains the error message.
        /// </returns>
        public static List<Tuple<int, string>> CompareDeep(this DataTable table, DataTable dataTable)
        {
            List<Tuple<int, string>> difference = new List<Tuple<int, string>>();
            if (!IsRowCountEqual(table, dataTable))
            {

                difference.Add(new Tuple<int, string>(0, "Row count mismatch in table: " + table.TableName));

            }
            else if (!IsColumnCountEqual(table, dataTable))
            {

                difference.Add(new Tuple<int, string>(0, "Column count mismatch in table: " + table.TableName));
            }
            else
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    for (int j = 0; j < table.Columns.Count; j++)
                    {

                        if (!Compare(table, dataTable, i, j))
                        {
                            difference.Add(new Tuple<int, string>(i, ErrorMessages.WriteComparisonError(table, dataTable, i, j)));
                        }
                    }
                }
            }
            return difference;

        }

        /// <summary>
        /// Comapres all values of two Data Tables.
        /// </summary>
        /// <param name="table">Extension method type declaration.</param>
        /// <param name="query">Query whose results are will be compared.</param>
        /// <returns>
        /// A List whose each element is a pair of int and string.
        /// Int denotes the row index.
        /// String contains the error message.
        /// </returns>
        public static List<Tuple<int, string>> CompareDeep(this DataTable d, string query)
        {
            DataAccessLink link = new DataAccessLink();
            return CompareDeep(d, link.GetDataTable(query));
        }

        /// <summary>
        /// Comapres all values of two Data Tables.
        /// </summary>
        /// <param name="table">Extension method type declaration.</param>
        /// <param name="query">Query whose results are will be compared.</param>
        /// <param name="dbName">Name of database instance which on which query will run.</param>
        /// <returns>
        /// A List whose each element is a pair of int and string.
        /// Int denotes the row index.
        /// String contains the error message.
        /// </returns>
        public static List<Tuple<int, string>> CompareDeep(this DataTable d, string query, string dbName)
        {
            DataAccessLink link = new DataAccessLink();
            return CompareDeep(d, link.GetDataTable(query, dbName));
        }

        #region Private Methods

        private static bool AreBasicChecksPassed(DataTable guiTable, DataTable databaseTable)
        {
            bool isEqual = true;

            if (!(guiTable.Rows.Count == databaseTable.Rows.Count))
            {
                return isEqual = false;
            }

            if (!(guiTable.Columns.Count == databaseTable.Columns.Count))
            {
                return isEqual = false;
            }
            return isEqual;
        }

        private static bool IsRowCountEqual(DataTable guiTable, DataTable databaseTable)
        {
            bool isEqual = true;
            if (!(guiTable.Rows.Count == databaseTable.Rows.Count))
            {
                isEqual = false;
            }
            return isEqual;
        }

        private static bool IsColumnCountEqual(DataTable guiTable, DataTable databaseTable)
        {
            bool isEqual = true;
            if (!(guiTable.Columns.Count == databaseTable.Columns.Count))
            {
                isEqual = false;
            }
            return isEqual;
        }

        private static bool Compare(DataTable data, DataTable dataTable, int i, int j)
        {
            Type columnType = data.Columns[j].DataType;

            //special case where columnType is Decimal but underlying value is DB.Null..
            if (data.Rows[i][j].ToString() == System.DBNull.Value.ToString() || dataTable.Rows[i][j].ToString() == System.DBNull.Value.ToString())
            {
                return CompareString(data, dataTable, i, j);
            }
            // Checks for all likely column Types.
            else if (columnType == typeof(decimal) || columnType == typeof(double)
                    || columnType == typeof(float) || columnType == typeof(int))
            {
                return CompareDecimal(data, dataTable, i, j);
            }

            else
            {
                return CompareString(data, dataTable, i, j);
            }

        }

        private static bool CompareString(DataTable data, DataTable dataTable, int i, int j)
        {
            if (data.Rows[i][j].ToString() != dataTable.Rows[i][j].ToString())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private static bool CompareDecimal(DataTable data, DataTable dataTable, int i, int j)
        {
            return IsApproximatelyEqual(Convert.ToDecimal(data.Rows[i][j]), Convert.ToDecimal(dataTable.Rows[i][j]));
        }

        // Checks for equality taking precision and tolerence into account. 
        private static bool IsApproximatelyEqual(Decimal d, Decimal c)
        {

            int precValue = precisionValue;
            int errorTolerence = precisionTolerence;
            if (precValue >= 0)
            {


                //d = Decimal.Round(d, precValue);
                //c = Decimal.Round(c, precValue);
                d = TruncateValue(precValue, d);
                c = TruncateValue(precValue, c);
            }
            if (errorTolerence > 0)
            {
                return IsEqualWithTolerence(d, c);
            }
            else
            {
                return IsEqualWithoutTolerence(d, c);
            }

        }

        private static decimal TruncateValue(int precision, decimal value)
        {


            double pow = Math.Pow(10, precision);
            decimal power = Convert.ToDecimal(pow);
            return Math.Floor(value * power) / power;

        }



        private static bool IsEqualWithTolerence(decimal d, decimal c)
        {

            int precValue = precisionValue;
            int errorTolerence = precisionTolerence;

            int diff = Decimal.Compare(d, c);
            if (diff == 0)
            {
                return true;
            }

            else if (diff < 0)
            {
                if (c == 0)
                {
                    if (Math.Abs(d) > 1 - (errorTolerence / (decimal)100.00))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }

                }
                else if ((Math.Abs(c - d) / c) > 1 - (errorTolerence / (decimal)100.00))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                if (d == 0)
                {
                    if (Math.Abs(c) > 1 - (errorTolerence / (decimal)100.00))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }

                }
                else if ((Math.Abs(d - c) / d) > 1 - (errorTolerence / (decimal)100.00))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        private static bool IsEqualWithoutTolerence(decimal d, decimal c)
        {
            int diff = Decimal.Compare(d, c);
            if (diff == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

    }
}
