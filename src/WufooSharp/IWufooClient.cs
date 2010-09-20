using System.Collections.Generic;
namespace WufooSharp
{
    public interface IWufooClient {
        
        //forms
        Form GetFormById(string hash);
        Form GetFormById(string hash, bool includeTodayCount);
        IEnumerable<Form> GetAllForms();
        IEnumerable<Form> GetAllForms(bool includeTodayCount);

        //users
        IEnumerable<User> GetAllUsers();

        //reports
        IEnumerable<Report> GetAllReports();
        Report GetReportById(string hash);

        //widgets
        IEnumerable<Widget> GetWidgetsByReportId(string reportHash);

        //comments
        IEnumerable<Comment> GetCommentsByFormId(string formHash);
        int GetCommentCountByFormId(string formHash);

        //entries
        IEnumerable<Entry> GetEntriesByFormId(string formHash);
        IEnumerable<Entry> GetEntriesByFormId(string formHash, FilterBuilder filters, Sort sort);

        IEnumerable<Entry> GetEntriesByReportId(string reportHash);
        IEnumerable<Entry> GetEntriesByReportId(string reportHash, FilterBuilder filters, Sort sort);

        int GetEntryCountByFormId(string formHash);
        int GetEntryCountByFormId(string formHash, FilterBuilder filters);
        int GetEntryCountByReportId(string reportHash);
        int GetEntryCountByReportId(string reportHash, FilterBuilder filters);

        PostResponse PostEntry(string formHash, Entry entry);
        
        
        IEnumerable<Field> GetFieldsByFormId(string formHash);
        IEnumerable<Field> GetFieldsByFormId(string formHash, bool system); 
        IEnumerable<Field> GetFieldsByReportId(string reportHash);
        IEnumerable<Field> GetFieldsByReportId(string reportHash, bool system);

        string PutWebHook(string formHash, WebHook webHook);
        string DeleteWebhook(string formHash, string webhookHash);

    }
}