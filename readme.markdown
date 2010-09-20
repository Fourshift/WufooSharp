#WufooSharp - .Net Wufoo API Wrapper

##Notes
Paging handled by wrapper as needed when collection is iterated over.
Integration and unit tests coming soon.

###Known issues
DeleteWebhook response causes exception when parsing response - investigating with Wufoo team.




##Example Usage

`//get all entries
    var client = new WufooClient("host", "api-key");
    var firstForm = client.GetAllForms().First();
    var entries = client.GetEntriesByFormId(firstForm.Hash);`

`//post new entry
    var entry = new Entry();
    entry["Field1"] = "Hello";
    entry["Field2"] = "World";
    client.PostEntry(firstForm.Hash, entry);`
            
`    //filter and sort entries
    var filtered = client.GetEntriesByFormId(firstForm.Hash, new FilterBuilder().BeginsWith("Field1", "He").EndsWith("Field2", "rld"), new Sort("EntryId", SortDirection.Desc));
`
