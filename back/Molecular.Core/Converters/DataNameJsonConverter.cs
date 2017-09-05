using Molecular.FileStore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;

namespace Molecular.Converters
{
    public class DataNameJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {

            object result = null;

            if (reader.Value != null)
            {

                string name = string.Empty;
                name = reader.Value.ToString();

                var repository = typeof(Repositories<>).MakeGenericType(objectType);

                MethodInfo method = repository.GetTypeInfo().GetMethod("Get", BindingFlags.Static | BindingFlags.Public);

                result = method.Invoke(null, new object[] { name });

            }

            return result;

        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {

            var property = value.GetType().GetProperty("Name", BindingFlags.Instance | BindingFlags.Public);
            if (property != null)
            {
                object name = property.GetValue(value);

                if (name != null)
                {
                    writer.WriteValue(name);
                }
                else
                {
                    writer.WriteValue(string.Empty);
                }

            }
        }

    }

}
