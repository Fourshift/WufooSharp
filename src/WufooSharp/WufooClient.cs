using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace WufooSharp
{
    public class WufooClient : IWufooClient
    {
        private IWufooDataProvider _provider;
        private int _pageSize;

        public WufooClient(string name, string apiKey)
            : this(new WufooDataProvider(name, apiKey), Constants.DefaultPageSize)
        {
        }

        public WufooClient(IWufooDataProvider provider)
            : this(provider, Constants.DefaultPageSize)
        {

        }

        public WufooClient(IWufooDataProvider provider, int pageSize)
        {
            _provider = provider;
            _pageSize = pageSize;
        }


        public Form GetFormById(string hash)
        {
            return GetFormById(hash, false);
        }


        public Form GetFormById(string hash, bool includeTodayCount)
        {
            string data = _provider.GetFormById(hash, includeTodayCount);
            var forms = JsonConvert.DeserializeObject<FormsResponse>(data);
            return forms.Forms.SingleOrDefault();
        }

        public IEnumerable<Form> GetAllForms(bool includeTodayCount)
        {
            string data = _provider.GetAllForms(includeTodayCount);
            var forms = JsonConvert.DeserializeObject<FormsResponse>(data);
            return forms.Forms;
        }

        public IEnumerable<Form> GetAllForms()
        {
            return GetAllForms(false);
        }

        public IEnumerable<User> GetAllUsers()
        {
            string data = _provider.GetAllUsers();
            var users = JsonConvert.DeserializeObject<UsersResponse>(data);
            return users.Users;
        }

        public IEnumerable<Report> GetAllReports()
        {
            string data = _provider.GetAllReports();
            var reports = JsonConvert.DeserializeObject<ReportsResponse>(data);
            return reports.Reports;
        }


        public Report GetReportById(string hash)
        {
            string data = _provider.GetReportById(hash);
            var reports = JsonConvert.DeserializeObject<ReportsResponse>(data);
            return reports.Reports.SingleOrDefault();
        }

        public IEnumerable<Widget> GetWidgetsByReportId(string reportHash)
        {
            string data = _provider.GetWidgetsByReportId(reportHash);
            var widgets = JsonConvert.DeserializeObject<WidgetsResponse>(data);
            return widgets.Widgets;
        }


        public int GetCommentCountByFormId(string formHash)
        {
            string data = _provider.GetCommentCountByFormId(formHash);
            var count = JsonConvert.DeserializeObject<CountResponse>(data);
            return count.Count;
        }


        public IEnumerable<Field> GetFieldsByFormId(string formHash, bool system)
        {
            string data = _provider.GetFieldsByFormId(formHash, system);
            var fields = JsonConvert.DeserializeObject<FieldsResponse>(data);
            return fields.Fields;
        }

        public IEnumerable<Field> GetFieldsByReportId(string reportHash, bool system)
        {
            string data = _provider.GetFieldsByReportId(reportHash, system);
            var fields = JsonConvert.DeserializeObject<FieldsResponse>(data, new ReportFieldConverter());
            return fields.Fields;
        }

        public PostResponse PostEntry(string formHash, Entry entry)
        {
            var postData = entry.Responses;
            string data = _provider.PostEntry(formHash, postData);
            var response = JsonConvert.DeserializeObject<PostResponse>(data);
            return response;
        }

        public IEnumerable<Field> GetFieldsByFormId(string formHash)
        {
            return GetFieldsByFormId(formHash, false);
        }

        public IEnumerable<Field> GetFieldsByReportId(string reportHash)
        {
            return GetFieldsByReportId(reportHash, false);
        }


        public IEnumerable<Entry> GetEntriesByFormId(string formHash)
        {
            return GetEntriesByFormId(formHash, null, null);
        }

        public IEnumerable<Entry> GetEntriesByFormId(string formHash, FilterBuilder filters, Sort sort)
        {
            return new FormEntryEnumerator(_provider, formHash, _pageSize, 0, filters, sort);
        }

        public IEnumerable<Entry> GetEntriesByReportId(string reportHash)
        {
            return GetEntriesByReportId(reportHash, null, null);
        }

        public IEnumerable<Entry> GetEntriesByReportId(string reportHash, FilterBuilder filters, Sort sort)
        {
            return new ReportEntryEnumerator(_provider, reportHash, _pageSize, 0, filters, sort);
        }

        public int GetEntryCountByFormId(string formHash)
        {
            return GetEntryCountByFormId(formHash, null);
        }
        public int GetEntryCountByFormId(string formHash, FilterBuilder filters)
        {
            string data = _provider.GetEntryCountByFormId(formHash, filters);
            var count = JsonConvert.DeserializeObject<EntryCountResponse>(data);
            return count.EntryCount;
        }


        public IEnumerable<Comment> GetCommentsByFormId(string formHash)
        {
            return new CommentsEnumerator(_provider, formHash, _pageSize, 0);
        }

        public int GetEntryCountByReportId(string reportHash)
        {
            return GetEntryCountByReportId(reportHash, null);
        }

        public int GetEntryCountByReportId(string reportHash, FilterBuilder filters)
        {
            string data = _provider.GetEntryCountByReportId(reportHash, filters);
            var count = JsonConvert.DeserializeObject<CountResponse>(data);
            return count.Count;
        }

        public string PutWebHook(string formHash, WebHook webHook)
        {
            string data = _provider.PutWebhook(formHash, webHook);
            var result = JsonConvert.DeserializeObject<WebhookPutResponse>(data);
            return result.WebhookPutResult.Hash;
        }

        public string DeleteWebhook(string formHash, string webhookHash)
        {
            string data = _provider.DeleteWebhook(formHash, webhookHash);
            var result = JsonConvert.DeserializeObject<WebhookDeleteResponse>(data);
            return result.WebhookDeleteResult.Hash;
        }
    }

   

}