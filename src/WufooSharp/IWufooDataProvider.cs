using System.Collections.Generic;

namespace WufooSharp
{
    public interface IWufooDataProvider
    {
        //forms
        string GetAllForms(bool includeTodayCount);
        string GetFormById(string hash, bool includeTodayCount);

        //users
        string GetAllUsers();
        
        //reports
        string GetAllReports();
        string GetReportById(string hash);
        
        //widgets
        string GetWidgetsByReportId(string reportHash);

        //comments
        string GetCommentsByFormId(string formHash, int pageStart, int pageSize);
        string GetCommentCountByFormId(string formHash);

        //entries
        string GetEntriesByFormId(string formHash, int pageStart, int pageSize, FilterBuilder filters, Sort sort);
        string GetEntriesByReportId(string formHash, int pageStart, int pageSize, FilterBuilder filters, Sort sort);
        string GetEntryCountByFormId(string formId, FilterBuilder filters);
        string GetEntryCountByReportId(string formHash, FilterBuilder filters);
        string PostEntry(string formHash, IEnumerable<KeyValuePair<string, string>> postData);

        //fields
        string GetFieldsByFormId(string formHash, bool system);
        string GetFieldsByReportId(string reportHash, bool system);


        //webhooks
        string PutWebhook(string formHash, WebHook webHook);
        string DeleteWebhook(string formHash, string webhookHash);
    }
}
