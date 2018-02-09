using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Septa.PardakhtVaset.Client.Internals
{
    public class ObjectParser
    {
        public T Extract<T>(IDataRecord row) where T : class, new()
        {
            var record = new T();
            var type = typeof(T);
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            for (int i = 0; i < row.FieldCount; i++)
            {
                var columnName = row.GetName(i);
                var value = row.GetValue(i);
                var property = properties.FirstOrDefault(x => x.Name == columnName);
                property.SetValue(record, value, null);
            }

            return record;
        }

        public Dictionary<string, object> ParseArgs(object args)
        {
            var result = new Dictionary<string, object>();
            if (args != null)
            {
                var argsType = args.GetType();
                var properties = argsType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var property in properties)
                {
                    result.Add(property.Name, property.GetValue(args, null));
                }
            }

            return result;
        }
    }
}
