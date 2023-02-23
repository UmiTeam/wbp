using System;
using Umi.Wbp.Mvvm.Common;

namespace Umi.Wbp.Routers;

public class NavigationJournalEntry : INavigationJournalEntry
{
    public NavigationJournalEntry(string path, IParameters parameters){
        Path = path;
        Parameters = parameters;
    }

    public string Path { get; }
    public IParameters Parameters { get; }
}