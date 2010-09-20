using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WufooSharp
{

    public class ReportFieldConverter : JsonConverter
    {
        public override bool CanRead { get { return true; } }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject orig = JObject.Load(reader);
            JObject result = new JObject();
            //JObject fields = new JObject();
            var fields = new JArray();

            var tok = orig["Fields"];
            foreach (var a in tok.Children()) {
                var b = a.Children();
                fields.Add(b);
            }

            result.Add("Fields", fields);

            var outString = result.ToString();
            var actual = new FieldsResponse();
            JsonConvert.PopulateObject(result.ToString(), actual);

            return actual;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(FieldsResponse);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var entry = value as Entry;
            if (entry == null)
                return;
            var output = new { Fields = entry.Responses };
            JObject.Parse(JsonConvert.SerializeObject(output)).WriteTo(writer);
        }
    }

}
