using System;

namespace Umi.Wbp.Regions;

public class NavigationContext
{
    public NavigationContext(NavigationParameters parameters, Uri uri){
        Parameters = parameters;
        Uri = uri;
    }

    public NavigationParameters Parameters { get; }
    public Uri Uri { get; }
}