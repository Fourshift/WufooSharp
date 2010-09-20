using System;
using Newtonsoft.Json;

namespace WufooSharp
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Comment
    {
        public int CommentId { get; set; }

        public int EntryId { get; set; }
        
        public string Text { get; set; }
        
        public string CommentedBy { get; set; }
        
        public DateTime DateCreated { get; set; }
    }
}
