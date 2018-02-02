using Dapper;
using System;
using System.Data.SqlClient;

namespace Septa.PardakhtVaset.Client
{
    public class DapperDbCommandExecutor : IDbCommandExecutor
    {
        public DapperDbCommandExecutor(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("message", nameof(connectionString));
            }

            ConnectionString = connectionString;
        }

        public string ConnectionString { get; }

        public int Execute(string sql, object param)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Execute(sql, param);
            }
        }
    }
}
