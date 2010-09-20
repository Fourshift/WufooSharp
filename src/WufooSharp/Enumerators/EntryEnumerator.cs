using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace WufooSharp
{
    public abstract class EntryEnumerator : IEnumerable<Entry>
    {

        protected IWufooDataProvider Provider { get; private set; }

        protected string FormHash { get; private set; }
        protected int PageSize { get; private set; }
        protected FilterBuilder Filters { get; private set; }
        protected Sort Sort { get; private set; }

        private int _pageStart;

        public EntryEnumerator(IWufooDataProvider provider, string formHash, int pageSize, int pageStart, FilterBuilder filters, Sort sort)
        {
            Provider = provider;
            FormHash = formHash;
            PageSize = pageSize;
            _pageStart = pageStart;
            Filters = filters;
            Sort = sort;
        }
        public IEnumerator<Entry> GetEnumerator()
        {
            int ndx = _pageStart;
            int max = GetCount();
            bool done = max == 0;
            while (!done)
            {
                var entries = JsonConvert.DeserializeObject<EntriesResponse>(GetEntryData(ndx)).Entries;
                ndx += entries.Count();
                done = entries.Count() == 0 || ndx >= max;
                foreach (var entry in entries)
                {
                    yield return entry;
                }
            }
        }

        public abstract string GetEntryData(int page);
        public abstract int GetCount();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
