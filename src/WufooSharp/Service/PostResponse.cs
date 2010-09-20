using System;
using Newtonsoft.Json;

namespace WufooSharp
{
    [JsonObject(MemberSerialization.OptOut)]
    public class PostResponse
    {
        [JsonConverter(typeof(WufooBooleanConverter))]
        public bool Success { get; set; }
        public int EntryId { get; set; }
        public Uri EntryLink { get; set; }
        public string RedirectUrl { get; set; }
        public string ErrorText { get; set; }
        public FieldError[] FieldErrors { get; set; }
    }
}
