using System;
using Newtonsoft.Json;

namespace WufooSharp
{
    [JsonObject(MemberSerialization.OptOut)]
    public class User
    {
        [JsonProperty("User")]
        public string Username { get; set; }
        
        public string Email { get; set; }
        
        public double? TimeZone { get; set; }
        
        public string Company { get; set; }

        [JsonConverter(typeof(WufooBooleanConverter))]
        public bool IsAccountOwner { get; set; }

        [JsonConverter(typeof(WufooBooleanConverter))]
        public bool CreateForms { get { return _createForms || AdminAccess; } set { _createForms = value; } }
        private bool _createForms;

        [JsonConverter(typeof(WufooBooleanConverter))]
        public bool CreateReports { get { return _createReports || AdminAccess; } set { _createReports = value; } }
        private bool _createReports;

        [JsonConverter(typeof(WufooBooleanConverter))]
        public bool CreateThemes { get { return _createThemes || AdminAccess; } set { _createThemes = value; } }
        private bool _createThemes;

        [JsonConverter(typeof(WufooBooleanConverter))]
        public bool AdminAccess { get { return _adminAccess || IsAccountOwner; } set { _adminAccess = value; } }
        private bool _adminAccess;
        
        public string ApiKey { get; set; }

        public Uri LinkForms { get; set; }

        public Uri LinkReports { get; set; }

        public string Hash { get; set; }

        public Uri ImageUrlBig { get; set; }
        
        public Uri ImageUrlSmall { get; set; }
        
        public bool HttpsEnabled { get; set; }
    }
}
