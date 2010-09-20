using System;
using Newtonsoft.Json;

namespace WufooSharp
{
    public class WufooBooleanConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(bool) || objectType == typeof(bool?);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Boolean)
            {
                return serializer.Deserialize<bool>(reader);
            }
            if (reader.TokenType == JsonToken.Null || reader.Value == null)
            {
                return false;
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