using Newtonsoft.Json;

namespace WufooSharp
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Choice
    {
        public string Label { get; set; }
    }
}