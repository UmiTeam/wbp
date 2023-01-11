using Umi.Wbp.Core.Common;

namespace Umi.Wbp.Routers;

public interface INavigationJournalEntry
{
    string Path { get; }
    IParameters Parameters { get; }
}