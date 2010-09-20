using System;
using Newtonsoft.Json;

namespace WufooSharp
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Report
    {
        
        public string Name { get; set; }
        
        [JsonConverter(typeof(WufooBooleanConverter))]
        public bool IsPublic { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }
        
        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }
        
        public string Hash { get; set; }
        
        public Uri LinkFields { get; set; }
        
        public Uri LinkEntries { get; set; }
        
        public Uri LinkEntriesCount { get; set; }
        
        public Uri LinkWidgets { get; set; }
    }
}