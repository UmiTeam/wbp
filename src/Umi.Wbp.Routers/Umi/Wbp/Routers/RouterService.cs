using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Umi.Wbp.Core.Common;
using Umi.Wbp.Mvvm;
using Volo.Abp.DependencyInjection;

namespace Umi.Wbp.Routers;

public class RouterService : IRouterService, ISingletonDependency
{
    private readonly Stack<INavigationJournalEntry> backStack = new();
    private readonly Stack<INavigationJournalEntry> forwardStack = new();

    private readonly IOptions<WbpRouterOptions> wbpRouterOptions;
    private readonly IServiceProvider serviceProvider;

    public RouterService(IOptions<WbpRouterOptions> wbpRouterOptions, IServiceProvider serviceProvider){
        this.wbpRouterOptions = wbpRouterOptions;
        this.serviceProvider = serviceProvider;
    }

    public void Push(string url){
        Push(url, new Parameters());
    }

    public void Push(string url, IParameters navigationParameters){
        var routerHost = serviceProvider.GetRequiredService<IRouterHost>();

        NavigationContext navigationContext = new(navigationParameters, CurrentEntry?.Path, url);

        wbpRouterOptions.Value.BeforeEach?.Invoke(navigationContext, newUrl =>
        {
            navigationContext = new(navigationContext.Parameters, CurrentEntry?.Path, newUrl);
            foreach (var (routerView, targetView) in GetNavigateRouters(null, newUrl, wbpRouterOptions.Value.Routes, routerHost.RouterViews, navigationContext)){
                if (routerView.Content == targetView && navigationParameters == CurrentEntry?.Parameters){
                    continue;
                }

                routerView.Content = targetView;

                if (targetView is INavigationAware navigationAware){
                    navigationAware.OnNavigatedTo(navigationContext);
                }

                if (targetView is FrameworkElement { DataContext: INavigationAware viewModelNavigationAware } and not IViewModelForSelf){
                    viewModelNavigationAware.OnNavigatedTo(navigationContext);
                }
            }

            var navigationJournalEntry = new NavigationJournalEntry(url, navigationParameters);
            RecordNavigation(navigationJournalEntry);

            wbpRouterOptions.Value.AfterEach?.Invoke(navigationContext);
        });
    }

    public bool CanGoBack => backStack.Count > 0;
    public bool CanGoForward => forwardStack.Count > 0;

    public INavigationJournalEntry CurrentEntry { get; private set; }

    public bool GoBack(){
        if (!CanGoBack) return false;
        var journalEntry = backStack.Peek();
        var previousCurrentEntry = CurrentEntry;
        Push(journalEntry.Path, journalEntry.Parameters);
        forwardStack.Push(previousCurrentEntry);
        backStack.Pop();
        backStack.Pop();
        return true;
    }

    public bool GoForward(){
        if (!CanGoForward) return false;
        var journalEntry = forwardStack.Peek();
        Push(journalEntry.Path, journalEntry.Parameters);
        forwardStack.Pop();
        return true;
    }

    private void RecordNavigation(INavigationJournalEntry entry){
        if (CurrentEntry != null) backStack.Push(CurrentEntry);
        CurrentEntry = entry;
    }


    ICollection<(RouterView, object)> GetNavigateRouters(string parentUrl, string url, IEnumerable<Route> routes, IEnumerable<RouterView> routerViews, NavigationContext navigationContext){
        var needNavigateRouterViews = new List<(RouterView, object)>();
        var targetUri = new Uri($"{wbpRouterOptions.Value.BasePath}{url}/");
        foreach (var route in routes){
            Uri routeUri = new($"{wbpRouterOptions.Value.BasePath}{parentUrl}{route.Path}/");
            // Query satisfied route
            if (!routeUri.IsBaseOf(targetUri)) continue;
            foreach (var routerView in routerViews){
                var hasRoute = false;
                foreach (var (routerViewName, viewType) in route.GetComponents()){
                    if (routerView.RouterName != routerViewName) continue;
                    hasRoute = true;

                    var targetView = routerView.Content?.GetType() == viewType ? routerView.Content : serviceProvider.GetService(viewType);

                    needNavigateRouterViews.Add((routerView, targetView));

                    // Get child router
                    var childRouter = GetChildRouter(targetView);
                    if (!targetUri.IsBaseOf(routeUri) && route.Children.Count > 0){
                        // Navigate child router
                        if (childRouter.Any()){
                            needNavigateRouterViews.AddRange(GetNavigateRouters(parentUrl + route.Path, url, route.Children, childRouter, navigationContext));
                        }
                    }
                    else{
                        needNavigateRouterViews.AddRange(childRouter.Select(childRouterView => ((RouterView, object))(childRouterView, null)));
                    }

                    break;
                }

                if (!hasRoute){
                    //No route router
                    needNavigateRouterViews.Add((routerView, null));
                }
            }
        }

        return needNavigateRouterViews;
    }

    private IEnumerable<RouterView> GetChildRouter(object control){
        List<RouterView> routerControls = new();
        if (control is FrameworkElement frameworkElement)
            foreach (var child in LogicalTreeHelper.GetChildren(frameworkElement)){
                if (child is RouterView childRouterControl){
                    routerControls.Add(childRouterControl);
                    continue;
                }

                routerControls.AddRange(GetChildRouter(child));
            }

        return routerControls;
    }
}