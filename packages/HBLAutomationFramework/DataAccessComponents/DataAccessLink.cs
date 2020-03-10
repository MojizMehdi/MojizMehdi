using DataAccessComponent.Interface;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessComponent
{
    public class DataAccessLink : IDataAccessInterface
    {
        /// <summary>
        /// For getting a Database object from databaseName
        /// </summary>
        /// <param name="instanceName">Database name in DbApp.config</param>
        /// <returns>A new Database object</returns>
        private Database GetDatabase(string instanceName)
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            return factory.Create(instanceName);
        }

        /// <summary>
        /// For getting the default database object.
        /// </summary>
        /// <returns>A new Database object</returns>
        private Database GetDatabase()
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            return factory.CreateDefault();
        }
        public virtual DataTable GetDataTable(string query, string instanceName)
        {
            Database db = this.GetDatabase(instanceName);
            DbConnection connection = db.CreateConnection();
            DbCommand command = db.GetSqlStringCommand(query);
            connection.Open();
            DataSet dataSet = db.ExecuteDataSet(command);
            connection.Close();
            return dataSet.Tables[0];
        }

        public virtual DataTable GetDataTable(string query)
        {
            Database db = this.GetDatabase();
            DbConnection connection = db.CreateConnection();
            DbCommand command = db.GetSqlStringCommand(query);
            connection.Open();
            DataSet dataSet = db.ExecuteDataSet(command);
            connection.Close();
            return dataSet.Tables[0];
        }

        public virtual int GetNonQueryResult(string query, string instanceName)
        {
            Database db = this.GetDatabase(instanceName);
            DbConnection connection = db.CreateConnection();
            DbCommand command = db.GetSqlStringCommand(query);
            connection.Open();
            int result = db.ExecuteNonQuery(command);
            connection.Close();
            return result;
        }
    }
}
