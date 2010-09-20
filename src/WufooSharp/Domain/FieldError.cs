using Newtonsoft.Json;

namespace WufooSharp
{
    [JsonObject(MemberSerialization.OptOut)]
    public class FieldError
    {
        public string Id { get; set; }
        public string ErrorText { get; set; }
    }
}