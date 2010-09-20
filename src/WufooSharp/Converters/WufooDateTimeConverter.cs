using System;
using Newtonsoft.Json;

namespace WufooSharp
{
    public class WufooDateTime : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime) || objectType == typeof(DateTime?);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Date) {
                return serializer.Deserialize<DateTime>(reader);
            }
            if (reader.TokenType == JsonToken.Null || reader.Value == null)
            {
                return default(DateTime);
            }
            if (reader.TokenType == JsonToken.Integer || reader.TokenType == JsonToken.String)
            {
                return (reader.Value ?? 0).ToString() == "1";
            }
            throw new Exception("Cannot convert token type " + reader.TokenType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteComment(value.ToString());
        }
    }
}