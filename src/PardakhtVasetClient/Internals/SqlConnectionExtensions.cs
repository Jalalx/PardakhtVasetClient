using System.Collections.Generic;
using System.Data.SqlClient;

namespace Septa.PardakhtVaset.Client.Internals
{
    public static class SqlConnectionExtensions
    {
        public static int Execute(this SqlConnection connection, string sql, object args = null)
        {
            var parser = new ObjectParser();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;
                foreach (var param in parser.ParseArgs(args))
                {
                    var key = ParseKey(param);
                    command.Parameters.AddWithValue(key, param.Value);
                }

                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                return command.ExecuteNonQuery();
            }
        }

        public static object ExecuteScalar(this SqlConnection connection, string sql, object args = null)
        {
            var parser = new ObjectParser();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;
                foreach (var param in parser.ParseArgs(args))
                {
                    var key = ParseKey(param);
                    command.Parameters.AddWithValue(key, param.Value);
                }

                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                return command.ExecuteScalar();
            }
        }


        public static IEnumerable<T> Query<T>(this SqlConnection connection, string query, object args = null) where T : class, new()
        {
            var result = new List<T>();
            var parser = new ObjectParser();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = query;
                foreach (var param in parser.ParseArgs(args))
                {
                    var key = ParseKey(param);
                    command.Parameters.AddWithValue(key, param.Value);
                }

                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var record = parser.Extract<T>(reader);
                        result.Add(record);
                    }
                }
            }

            return result;
        }

        private static string ParseKey(KeyValuePair<string, object> param)
        {
            var key = param.Key;
            if (key.StartsWith("@"))
                key = key.Substring(1);
            key = "@" + key;

            return key;
        }
    }
}
