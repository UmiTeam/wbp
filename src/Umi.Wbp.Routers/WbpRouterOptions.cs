using System;
using System.Collections.Generic;

namespace Umi.Wbp.Routers;

public class WbpRouterOptions
{
    public ICollection<Route> Routes { get; set; }
    public event EventHandler<NavigationEventArgs> Navigating;
    public event EventHandler<NavigationEventArgs> Navigated;
    public event EventHandler<NavigationFailedEventArgs> NavigationFailed;

    internal void RaiseNavigating(object sender, NavigationContext navigationContext){
        Navigating?.Invoke(sender, new NavigationEventArgs(navigationContext));
    }

    internal void RaiseNavigated(object sender, NavigationContext navigationContext){
        Navigated?.Invoke(sender, new NavigationEventArgs(navigationContext));
    }

    internal void RaiseNavigationFailed(object sender, NavigationContext navigationContext, Exception error){
        NavigationFailed?.Invoke(sender, new NavigationFailedEventArgs(navigationContext, error));
    }

    internal string BasePath { get; init; } = "http://localhost";
}

public class Route
{
    public string Path { get; init; }
    public Type Component { get; init; }
    public Dictionary<string, Type> Components { get; init; } = new();
    public ICollection<Route> Children { get; init; } = new List<Route>();

    internal Dictionary<string, Type> GetComponents(){
        if (Component != null) Components.TryAdd(RouterView.DefaultRouterViewName, Component);
        return Components;
    }
}