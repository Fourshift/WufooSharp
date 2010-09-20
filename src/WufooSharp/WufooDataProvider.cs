using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Collections;
using System.Reflection;
using System.Web;
using System.Linq;

namespace WufooSharp
{
    public class WufooDataProvider : IWufooDataProvider
    {
        private readonly string _apiKey;
        private readonly string _name;
        private readonly string _baseUrl;
        private static readonly string __latestVersion = "v3";

        public WufooDataProvider(string name, string apiKey, string apiVersion)
        {
            _name = name;
            _apiKey = apiKey;
            _baseUrl = string.Format("https://{0}.wufoo.com/api/{1}/", name, apiVersion);
        }

        public WufooDataProvider(string name, string apiKey)
            : this(name, apiKey, __latestVersion) { }

        public string GetAllForms(bool includeTodayCount)
        {
            string url = _baseUrl + "forms.json";
            var param = new Dictionary<string, object>();
            if (includeTodayCount)
            {
                param["includeTodayCount"] = includeTodayCount.ToString().ToLower();
            }
            return GetData(url, param);
        }

        public string GetFormById(string formHash, bool includeTodayCount)
        {
            string url = _baseUrl + "forms/{0}.json".FormatWith(formHash);
            var param = new Dictionary<string, object>();
            if (includeTodayCount)
            {
                param["includeTodayCount"] = includeTodayCount.ToString().ToLower();
            }
            return GetData(url, param);
        }

        public string GetAllUsers()
        {
            string url = _baseUrl + "users.json";
            return GetData(url);
        }

        public string GetAllReports()
        {
            string url = _baseUrl + "reports.json";
            return GetData(url);
        }

        public string GetReportById(string reportHash)
        {
            string url = _baseUrl + "reports/{0}.json".FormatWith(reportHash);
            return GetData(url);
        }

        public string GetWidgetsByReportId(string reportHash)
        {
            string url = _baseUrl + "reports/{0}/widgets.json".FormatWith(reportHash);
            return GetData(url);
        }

        public string GetCommentsByFormId(string formHash, int pageStart, int pageSize)
        {
            string url = _baseUrl + "forms/{0}/comments.json".FormatWith(formHash);
            return GetData(url, new { pageStart = pageStart, pageSize =pageSize } );
        }

        public string GetCommentCountByFormId(string formHash)
        {
            string url = _baseUrl + "forms/{0}/comments/count.json".FormatWith(formHash);
            return GetData(url);
        }

        public string GetEntriesByFormId(string formHash, int pageStart, int pageSize, FilterBuilder filters, Sort sort)
        {
            string url = _baseUrl + "forms/{0}/entries.json".FormatWith(formHash);
            return GetEntries(url, pageStart, pageSize, filters, sort);
        }

        public string GetEntriesByReportId(string reportHash, int pageStart, int pageSize, FilterBuilder filters, Sort sort)
        {
            string url = _baseUrl + "reports/{0}/entries.json".FormatWith(reportHash);
            return GetEntries(url, pageStart, pageSize, filters, sort);
        }

        public string GetEntryCountByFormId(string formHash, FilterBuilder filters)
        {
            string url = _baseUrl + "forms/{0}/entries/count.json".FormatWith(formHash);
            var param = new Dictionary<string, object>();
            foreach (var kvp in GetQueryParams(filters))
            {
                param[kvp.Key] = kvp.Value;
            }
            return GetData(url, param);
        }

        public string GetEntryCountByReportId(string formHash, FilterBuilder filters)
        {
            string url = _baseUrl + "reports/{0}/entries/count.json".FormatWith(formHash);
            var param = new Dictionary<string, object>();
            foreach (var kvp in GetQueryParams(filters))
            {
                param[kvp.Key] = kvp.Value;
            }
            return GetData(url, param);
        }

        public string PostEntry(string formHash, IEnumerable<KeyValuePair<string, string>> postData)
        {
            string url = _baseUrl + "forms/{0}/entries.json".FormatWith(formHash);
            return PostData(url, postData);
        }

        public string PutWebhook(string formHash, WebHook webHook)
        {
            string url = _baseUrl + "forms/{0}/webhooks.json".FormatWith(formHash);
            var postData = new Dictionary<string, string>();
            postData["url"] = webHook.Url;
            if (!string.IsNullOrEmpty(webHook.HandshakeKey)) { 
                postData["handshakekey"] = webHook.HandshakeKey;
            }
            if (webHook.MetaData) { 
                postData["metadata"] = "true";
            }
            
            return PutData(url,postData);
        }

        public string DeleteWebhook(string formHash, string hookHash) {
            string url = _baseUrl + "forms/{0}/webhooks/{1}.json".FormatWith(formHash, hookHash);
            var param = new Dictionary<string, string>();
            param["hash"] = hookHash;
            return DeleteData(url, param);
        }

        public string GetFieldsByFormId(string formHash, bool system)
        {
            string url = _baseUrl + "forms/{0}/fields.json".FormatWith(formHash);
            var param = new Dictionary<string, object>();
            if (system)
            {
                param["system"] = system.ToString().ToLower();
            }
            return GetData(url, param);
        }

        public string GetFieldsByReportId(string reportHash, bool system)
        {
            string url = _baseUrl + "reports/{0}/fields.json".FormatWith(reportHash);
            var param = new Dictionary<string, object>();
            if (system)
            {
                param["system"] = system.ToString().ToLower();
            }
            return GetData(url, param);
        }

        private string GetData(string url)
        {
            HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
            req.UserAgent = Constants.UserAgent;
            req.Credentials = new NetworkCredential(_apiKey, _apiKey);
            using (StreamReader sr = new StreamReader(req.GetResponse().GetResponseStream()))
            {
                return sr.ReadToEnd();
            }
        }

        private string GetData(string url, object obj) {
            var dict = new Dictionary<string, object>();

            foreach (var property in (obj ?? new object()).GetType().GetProperties()) {
                if (property.CanRead) {
                    var value = property.GetValue(obj, null);
                    if (value != null) {
                        dict.Add(property.Name, value);
                    }
                }
            }
            return GetData(url, dict);
        }

        private string GetData(string url, IDictionary dict)
        {
            string queryString = string.Empty;
            List<string> parts = new List<string>();

            if (dict != null)
            {
                foreach (var key in dict.Keys)
                {
                    parts.Add("{0}={1}".FormatWith(key, (dict[key] ?? string.Empty).ToString()));
                }
            }
            if (parts.Count > 0)
            {
                queryString = "?" + string.Join("&", parts.ToArray());
            }

            return GetData(url + queryString);
        }


        private string GetEntries(string url, int pageStart, int pageSize, FilterBuilder filters, Sort sort)
        {
            Dictionary<string, object> param = new Dictionary<string, object>() { { "pageStart", pageStart }, { "pageSize", pageSize } };
            foreach (var kvp in GetQueryParams(sort))
            {
                param[kvp.Key] = kvp.Value;
            }
            foreach (var kvp in GetQueryParams(filters))
            {
                param[kvp.Key] = kvp.Value;
            }
            return GetData(url, param);
        }

        private IEnumerable<KeyValuePair<string, object>> GetQueryParams(FilterBuilder filterBuilder)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            var filters = (filterBuilder ?? new FilterBuilder()).GetFilters();
            for (int i = 0; i < filters.Length; i++)
            {
                param["Filter" + (i+1)] = filters[i].ToString();
            }
            return param;
        }

        private IEnumerable<KeyValuePair<string, object>> GetQueryParams(Sort sort)
        {
            var ret = new Dictionary<string, object>();
            if (sort != null)
            {
                ret["sortDirection"] = sort.Direction.ToString().ToUpper();
                ret["sort"] = sort.Field;
            }
            return ret;
        }
        private string PutData(string url, IEnumerable<KeyValuePair<string, string>> postData) { 
           return SendData(url,postData,"PUT");
        }
        private string PostData(string url, IEnumerable<KeyValuePair<string, string>> postData) {
            return SendData(url, postData, "POST");
        }
        private string DeleteData(string url, IEnumerable<KeyValuePair<string, string>> postData) {
            return SendData(url, postData, "DELETE");
        }
        private string SendData(string url, IEnumerable<KeyValuePair<string, string>> postData, string method)
        {
            string strData = string.Join("&", postData.Select(x => string.Format("{0}={1}", HttpUtility.UrlEncode(x.Key), HttpUtility.UrlEncode(x.Value))).ToArray());
            var data = Encoding.UTF8.GetBytes(strData);

            var req = WebRequest.Create(url);
            req.ContentType = "multipart/form-data";
            req.Method = method;
            req.ContentLength = data.Length;
            req.Credentials = new NetworkCredential(_apiKey, _apiKey);

            using (Stream requestStream = req.GetRequestStream())
            {
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();
            }

            using (StreamReader reader = new StreamReader(req.GetResponse().GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        }



    }
}