using System;
using Umi.Wbp.Core.Common;

namespace Umi.Wbp.Routers;

public interface IRouterService
{
    void Push(string url);
    void Push(string url, IParameters navigationParameters);
    bool CanGoBack { get; }
    bool CanGoForward { get; }
    INavigationJournalEntry CurrentEntry { get; }
    bool GoBack();
    bool GoForward();
}