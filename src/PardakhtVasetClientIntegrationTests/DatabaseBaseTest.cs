using Septa.PardakhtVaset.Client.Internals;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PardakhtVasetClientIntegrationTests
{
    public abstract class DatabaseBaseTest : IDisposable
    {
        protected string ConnectionStrings
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            }
        }

        protected SqlConnection CreateConnection()
        {
            return new SqlConnection(ConnectionStrings);
        }

        protected bool TableExists(string tableName)
        {
            using (var conneciton = CreateConnection())
            {
                var adapter = new SqlDataAdapter($"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'{tableName}'", conneciton);
                var dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable.Rows.Count == 1 && dataTable.Rows[0][0].Equals(tableName);
            }
        }

        public abstract void Dispose();
    }
}
