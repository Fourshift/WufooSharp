using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WufooSharp
{

    public class EntryConverter : JsonConverter
    {
        public override bool CanRead { get { return true; } }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string[] props = new string[] { "EntryId", "DateCreated", "CreatedBy", "DateUpdated", "UpdatedBy" };

            JObject orig = JObject.Load(reader);
            JObject result = new JObject();
            JObject responses = new JObject();

            foreach (string p in props)
            {
                result[p] = orig[p];
                orig.Remove(p);
            }

            foreach (var prop in orig.Properties())
            {
                responses[prop.Name] = prop.Value;
            }
            result.Add("Fields", responses);

            var outString = result.ToString();
            Entry e = new Entry();
            JsonConvert.PopulateObject(result.ToString(), e);

            return e;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Entry);
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
