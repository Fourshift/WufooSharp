using System.Collections.Generic;
using Newtonsoft.Json;
namespace WufooSharp
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Field
    {
        public string Id { get; set; }
        
        public string Title { get; set; }

        [JsonConverter(typeof(WufooBooleanConverter))]
        public bool IsRequired { get; set; }

        [JsonConverter(typeof(WufooBooleanConverter))]
        public bool IsSystem { get; set; }
        
        public string Type { get; set; }

        public IEnumerable<SubField> SubFields { get; set; }
        
        public IEnumerable<Choice> Choices { get; set; }
    }
}
