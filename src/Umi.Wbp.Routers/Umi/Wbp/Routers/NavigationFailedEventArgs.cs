using System;

namespace Umi.Wbp.Routers;

public class NavigationFailedEventArgs : EventArgs
{
    public NavigationFailedEventArgs(NavigationContext navigationContext){
        NavigationContext = navigationContext ?? throw new ArgumentNullException(nameof(navigationContext));
    }


    public NavigationFailedEventArgs(NavigationContext navigationContext, Exception error) : this(navigationContext){
        Error = error;
    }


    public NavigationContext NavigationContext { get; private set; }


    public Exception Error { get; private set; }


    public Uri Uri => NavigationContext?.Uri;
}