using Newtonsoft.Json;

namespace WufooSharp
{

    public class ReportEntryEnumerator : EntryEnumerator
    {
        public ReportEntryEnumerator(IWufooDataProvider provider, string reportHash, int pageSize, int pageStart, FilterBuilder filters, Sort sort)
            : base(provider, reportHash, pageSize, pageStart, filters, sort) { }

        public override string GetEntryData(int page)
        {
            return Provider.GetEntriesByReportId(FormHash, page, PageSize, Filters, Sort);
        }

        public override int GetCount()
        {
            return JsonConvert.DeserializeObject<CountResponse>(Provider.GetEntryCountByReportId(FormHash, Filters)).Count;
        }
    }
}