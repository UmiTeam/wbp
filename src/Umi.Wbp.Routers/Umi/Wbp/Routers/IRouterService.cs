using System;

namespace Umi.Wbp.Routers;

public interface IRouterService
{
    void Push(string url, bool updateByParameters = true);
    void Push(string url, NavigationParameters navigationParameters, bool updateByParameters = true);
    void Push(string url, NavigationParameters navigationParameters, Action<NavigationResult> navigationCallback, bool updateByParameters = true);

    bool CanGoBack { get; }
    bool CanGoForward { get; }
    INavigationJournalEntry CurrentEntry { get; }
    bool GoBack();
    bool GoForward();
}