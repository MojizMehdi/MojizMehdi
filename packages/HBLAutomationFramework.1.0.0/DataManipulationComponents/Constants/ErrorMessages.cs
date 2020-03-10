using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DataManipulationComponents.Constants
{
    public static class ErrorMessages
    {
        public static string WriteComparisonError(DataTable dt, DataTable dataTable, int i, int j)
        {
            string errorMessage;
            List<string> str = new List<string>();
            str.Add(dt.TableName);
            str.Add(dt.Columns[j].ColumnName);
            str.Add(dt.Columns[0].ColumnName);
            str.Add(dt.Rows[i][0].ToString());
            str.Add(dt.Rows[i][j].ToString());
            str.Add(dt.TableName);
            str.Add(dataTable.Columns[j].ColumnName);
            str.Add(dataTable.Columns[0].ColumnName);
            str.Add(dataTable.Rows[i][0].ToString());
            str.Add(dataTable.Rows[i][j].ToString());

            foreach (string st in str)
            {
                if (st.Contains('{') || st.Contains('}'))
                {
                    st.Trim('{', '}');
                }
            }


            //return  errorMessage = string.Format("Description: Data Item mismatched.\n\n\nSource:\nTable Name:     "
            //                 + dt.TableName + "\nColumn Name:     " + dt.Columns[j].ColumnName + "\n"
            //                 + dt.Columns[0].ColumnName + ":     " + dt.Rows[i][0].ToString() + "\nValue:     "
            //                 + dt.Rows[i][j].ToString() +
            //                 "\n\nTarget:\nTable Name:     " + dt.TableName + "\nColumn Name:     " +dataTable.Columns[j].ColumnName + "\n"
            //                 + dataTable.Columns[0].ColumnName + ":     " + dataTable.Rows[i][0].ToString() + "\nValue:     "
            //                 + dataTable.Rows[i][j].ToString()+"\n\n\n");

            return errorMessage = string.Format("Description: Data Item mismatched.\n\n\nSource:\nTable Name:     "
                           + str[0] + "\nColumn Name:     " + str[1] + "\n"
                           + str[2] + ":     " + str[3] + "\nValue:     "
                           + str[4] +
                           "\n\nTarget:\nTable Name:     " + str[5] + "\nColumn Name:     " + str[6] + "\n"
                           + str[7] + ":     " + str[8] + "\nValue:     "
                           + str[9] + "\n\n\n");

            //
        }
    }
}
