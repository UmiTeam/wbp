using System;

namespace Umi.Wbp.Routers;

public class NavigationContext
{
    public NavigationContext(NavigationParameters parameters, Uri uri){
        Parameters = parameters;
        Uri = uri;
    }

    public NavigationParameters Parameters { get; }
    public Uri Uri { get; }
    public Type From { get; set; }
    public Type To { get; set; }
}