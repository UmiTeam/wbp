using System;

namespace Umi.Wbp.Routers;

public class NavigationEventArgs : EventArgs
{
    public NavigationEventArgs(NavigationContext navigationContext){
        NavigationContext = navigationContext ?? throw new ArgumentNullException(nameof(navigationContext));
    }


    public NavigationContext NavigationContext { get; private set; }

    public Uri Uri => NavigationContext?.Uri;
}