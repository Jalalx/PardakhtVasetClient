using Septa.PardakhtVaset.Client.Properties;
using System;

namespace Septa.PardakhtVaset.Client
{
    public class SqlServerDbInitializer : IDbInitializer
    {
        public string ConnectionString { get; }
        public IDbCommandExecutor DbCommandExecutor { get; }

        public SqlServerDbInitializer(string connectionString) : this(connectionString, new DapperDbCommandExecutor(connectionString))
        {
        }

        public SqlServerDbInitializer(string connectionString, IDbCommandExecutor dbCommandExecutor)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("message", nameof(connectionString));
            }

            ConnectionString = connectionString;
            DbCommandExecutor = dbCommandExecutor ?? throw new ArgumentNullException(nameof(dbCommandExecutor));
        }

        public void Init(string schema, string tablePrefix)
        {
            if (string.IsNullOrEmpty(schema))
                throw new ArgumentNullException(nameof(schema), "parameter can not be null or empty.");

            var script = ScriptResources.CreatePaymentLinksTableScript
                .Replace("$SCHEMA$", schema)
                .Replace("$TABLEPREFIX$", tablePrefix ?? string.Empty);

            DbCommandExecutor.Execute(script, null);
        }
    }
}
