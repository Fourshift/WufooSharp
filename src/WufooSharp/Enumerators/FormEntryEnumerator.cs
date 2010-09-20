using Newtonsoft.Json;

namespace WufooSharp
{
    public class FormEntryEnumerator : EntryEnumerator
    {
        public FormEntryEnumerator(IWufooDataProvider provider, string formHash, int pageSize, int pageStart, FilterBuilder filters, Sort sort)
            : base(provider, formHash, pageSize, pageStart, filters, sort) { }
        public override string GetEntryData(int page)
        {
            return Provider.GetEntriesByFormId(FormHash, page, PageSize, Filters, Sort);
        }

        public override int GetCount()
        {
            return JsonConvert.DeserializeObject<EntryCountResponse>(Provider.GetEntryCountByFormId(FormHash, Filters)).EntryCount;
        }
    }
}
