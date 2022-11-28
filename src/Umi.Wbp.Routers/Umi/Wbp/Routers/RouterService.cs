using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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

    public void Push(string url, bool forceUpdate = false){
        Push(url, new NavigationParameters(), null, forceUpdate);
    }

    public void Push(string url, NavigationParameters navigationParameters, bool forceUpdate = false){
        Push(url, navigationParameters, null, forceUpdate);
    }

    public void Push(string url, NavigationParameters navigationParameters, Action<NavigationResult> navigationCallback, bool forceUpdate = false){
        var routerHost = serviceProvider.GetRequiredService<IRouterHost>();
        Uri targetUri = new Uri($"{wbpRouterOptions.Value.BasePath}{url}/");
        NavigationContext navigationContext = new(navigationParameters, targetUri);
        wbpRouterOptions.Value.RaiseNavigating(this, navigationContext);
        if (Navigate(null, url, wbpRouterOptions.Value.Routes, routerHost.RouterViews, navigationContext, out var needNavigateRouterViews)){
            foreach (var (routerView, targetView) in needNavigateRouterViews){
                if (routerView.Content == targetView && !forceUpdate){
                    continue;
                }

                MvvmHelper.CallViewAndViewModelAction<INavigationAware>(routerView.Content,
                    action => action.OnNavigatedFrom(navigationContext));

                routerView.Content = targetView;

                MvvmHelper.CallViewAndViewModelAction<INavigationAware>(targetView,
                    action => action.OnNavigatedTo(navigationContext));
            }

            wbpRouterOptions.Value.RaiseNavigated(this, navigationContext);

            NavigationJournalEntry navigationJournalEntry = new NavigationJournalEntry(url, navigationParameters);
            RecordNavigation(navigationJournalEntry);

            NavigationResult navigationResult = new(navigationContext, true);
            navigationCallback?.Invoke(navigationResult);
        }
        else{
            NavigationResult navigationResult = new(navigationContext, false);
            navigationCallback?.Invoke(navigationResult);
            wbpRouterOptions.Value.RaiseNavigationFailed(this, navigationContext, navigationResult.Error);
        }
    }

    public bool CanGoBack => backStack.Count > 0;
    public bool CanGoForward => forwardStack.Count > 0;

    public INavigationJournalEntry CurrentEntry { get; private set; }

    public bool GoBack(){
        bool result = true;
        if (!CanGoBack) return false;
        var journalEntry = backStack.Peek();
        var previousCurrentEntry = CurrentEntry;
        Push(journalEntry.Path, journalEntry.Parameters, navigationResult =>
        {
            if (navigationResult.Result == true){
                forwardStack.Push(previousCurrentEntry);
                backStack.Pop();
                backStack.Pop();
            }
            else result = false;
        });
        return result;
    }

    public bool GoForward(){
        bool result = true;
        if (!CanGoForward) return false;
        var journalEntry = forwardStack.Peek();
        Push(journalEntry.Path, journalEntry.Parameters, navigationResult =>
        {
            if (navigationResult.Result == true){
                forwardStack.Pop();
            }
            else result = false;
        });
        return result;
    }

    private void RecordNavigation(INavigationJournalEntry entry){
        if (CurrentEntry != null) backStack.Push(CurrentEntry);
        CurrentEntry = entry;
    }

    private bool Navigate(string parentUrl, string url, IEnumerable<Route> routes, IEnumerable<RouterView> routerViews, NavigationContext navigationContext, out ICollection<(RouterView, object)> needNavigateRouterViews, bool forceUpdate = false){
        needNavigateRouterViews = new List<(RouterView, object)>();
        Uri targetUri = new Uri($"{wbpRouterOptions.Value.BasePath}{url}/");
        foreach (var route in routes){
            Uri routeUri = new($"{wbpRouterOptions.Value.BasePath}{parentUrl}{route.Path}/");
            // Query satisfied route
            if (routeUri.IsBaseOf(targetUri)){
                foreach (var routerView in routerViews){
                    bool hasRoute = false;
                    foreach (var (routerViewName, viewType) in route.GetComponents()){
                        if (routerView.RouterName == routerViewName){
                            hasRoute = true;

                            if (routerView.Content?.GetType() != viewType || navigationContext.Parameters != CurrentEntry?.Parameters){
                                if (!MvvmHelper.CallViewAndViewModelAction<INavigationAware>(routerView.Content, x => x.OnNavigatingFrom(navigationContext))){
                                    return false;
                                }
                            }

                            object targetView;
                            if (routerView.Content?.GetType() == viewType){
                                targetView = routerView.Content;
                            }
                            else{
                                targetView = serviceProvider.GetService(viewType);
                            }

                            if (routerView.Content != targetView || navigationContext.Parameters != CurrentEntry?.Parameters){
                                if (!MvvmHelper.CallViewAndViewModelAction<INavigationAware>(targetView, x => x.OnNavigatingTo(navigationContext))){
                                    return false;
                                }
                            }

                            needNavigateRouterViews.Add((routerView, targetView));

                            // Get child router
                            var childRouter = GetChildRouter(targetView);
                            if (!targetUri.IsBaseOf(routeUri) && route.Children.Count > 0){
                                // Navigate child router
                                if (childRouter.Any()){
                                    Navigate(parentUrl + route.Path, url, route.Children, childRouter, navigationContext, out var needNavigateRouterViews2);
                                    foreach (var needNavigateRouterView in needNavigateRouterViews2){
                                        needNavigateRouterViews.Add(needNavigateRouterView);
                                    }
                                }
                            }
                            else{
                                foreach (var childRouterView in childRouter){
                                    if (!MvvmHelper.CallViewAndViewModelAction<INavigationAware>(childRouterView.Content, x => x.OnNavigatingFrom(navigationContext))){
                                        return false;
                                    }

                                    needNavigateRouterViews.Add((childRouterView, null));
                                }
                            }

                            break;
                        }
                    }

                    if (!hasRoute){
                        //No route router
                        if (!MvvmHelper.CallViewAndViewModelAction<INavigationAware>(routerView.Content, x => x.OnNavigatingFrom(navigationContext))){
                            return false;
                        }

                        needNavigateRouterViews.Add((routerView, null));
                    }
                }
            }
        }

        return true;
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