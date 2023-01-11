using Umi.Wbp.Core.Common;

namespace Umi.Wbp.Routers;

public class NavigationContext
{
    public NavigationContext(IParameters parameters, string from, string to){
        Parameters = parameters;
        From = from;
        To = to;
    }

    public IParameters Parameters { get; }
    public string From { get; }
    public string To { get; }
}