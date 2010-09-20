using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
namespace WufooSharp
{

    [JsonObject(MemberSerialization.OptOut)]
    [JsonConverter(typeof(EntryConverter))]
    public class Entry
    {
        public Entry() { }
        public Entry(IDictionary<string, string> responses)
        {
            _properties = responses;
        }
        public int EntryId { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? DateUpdated { get; set; }
        public string UpdatedBy { get; set; }

        [JsonIgnore]
        public IEnumerable<KeyValuePair<string, string>> Responses { get { return _properties.ToArray(); } }

        public string this[int ndx]
        {
            get { return this["Field" + ndx]; }
            set { this["Field" + ndx] = value; }
        }

        public string this[string ndx]
        {
            get
            {
                return _properties[ndx];
            }
            set
            {
                _properties[ndx] = value;
            }
        }
        [JsonProperty("Fields")]
        private IDictionary<string, string> _properties = new Dictionary<string, string>();
    }
}