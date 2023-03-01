using System;
using System.Collections.Generic;

namespace Umi.Wbp.Routers;

public class WbpRouterOptions
{
    public ICollection<Route> Routes { get; set; }

    public Action<NavigationContext, Action<string, bool>> BeforeEach { get; set; } = (context, next) => { next?.Invoke(context.To, true); };
    public Action<NavigationContext> AfterEach { get; set; }
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