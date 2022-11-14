using System;

namespace Umi.Wbp.Routers;

public class NavigationJournalEntry : INavigationJournalEntry
{
    public NavigationJournalEntry(string path, NavigationParameters parameters){
        Path = path;
        Parameters = parameters;
    }

    public string Path { get; }
    public NavigationParameters Parameters { get; }
}