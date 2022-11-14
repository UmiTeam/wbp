using System;

namespace Umi.Wbp.Routers;

public interface IRouterService
{
    void Push(string url);
    void Push(string url, NavigationParameters navigationParameters);
    void Push(string url, NavigationParameters navigationParameters, Action<NavigationResult> navigationCallback);

    bool CanGoBack { get; }
    bool CanGoForward { get; }
    INavigationJournalEntry CurrentEntry { get; }
    bool GoBack();
    bool GoForward();
}