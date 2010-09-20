using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace WufooSharp
{

    public class CommentsEnumerator : IEnumerable<Comment>
    {
        private IWufooDataProvider _provider;

        private int _pageSize;
        private int _pageStart;
        private string _formHash;

        public CommentsEnumerator(IWufooDataProvider provider, string formHash, int pageSize, int pageStart)
        {
            _provider = provider;
            _pageSize = pageSize;
            _pageStart = pageStart;
            _formHash = formHash;
        }

        public IEnumerator<Comment> GetEnumerator()
        {
            bool done = false;
            int ndx = _pageStart;
            while (!done)
            {
                var comments = JsonConvert.DeserializeObject<CommentsResponse>(_provider.GetCommentsByFormId(_formHash, ndx, _pageSize)).Comments;
                ndx += comments.Count();
                done = comments.Count() == 0;
                foreach (var comment in comments)
                {
                    yield return comment;
                }
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

}
