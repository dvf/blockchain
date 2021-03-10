using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlockChain
{
    public class ObjectToPropertyConverter : JsonConverter
    {
        private readonly Type type;
        private readonly string propertyName;
        public ObjectToPropertyConverter(Type type, string propertyName)
        {
            this.type = type;
            this.propertyName = propertyName;
        }

        public override bool CanConvert(Type objectType)
        {
            return type == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object obj, JsonSerializer serializer)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                writer.WriteValue(obj.ToString());
            }
            else
            {
                var value = type.GetProperty(propertyName).GetValue(obj, null);
                if (value != null)
                {
                    writer.WriteValue(value.ToString());
                }
            }
        }
    }
}
