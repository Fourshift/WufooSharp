using Newtonsoft.Json;

namespace WufooSharp
{
    [JsonObject(MemberSerialization.OptOut)]
    public class SubField
    {
        public string Id { get; set; }

        public string Label { get; set; }
    }
}