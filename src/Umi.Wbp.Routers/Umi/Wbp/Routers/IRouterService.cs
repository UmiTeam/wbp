using System;

namespace Umi.Wbp.Routers;

public interface IRouterService
{
    void Push(string url, bool forceUpdate = false);
    void Push(string url, NavigationParameters navigationParameters, bool forceUpdate = false);
    void Push(string url, NavigationParameters navigationParameters, Action<NavigationResult> navigationCallback, bool forceUpdate = false);

    bool CanGoBack { get; }
    bool CanGoForward { get; }
    INavigationJournalEntry CurrentEntry { get; }
    bool GoBack();
    bool GoForward();
}