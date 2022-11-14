using System;

namespace Umi.Wbp.Routers;

public interface INavigationJournalEntry
{
    string Path { get; }
    NavigationParameters Parameters { get; }
}