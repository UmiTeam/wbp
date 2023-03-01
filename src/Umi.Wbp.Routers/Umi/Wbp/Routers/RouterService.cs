using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Umi.Wbp.Mvvm.Common;
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
        wbpRouterOptions.Value.BeforeEach?.Invoke(navigationContext, (newUrl, canNavigate) =>
        {
            if (!canNavigate) return;
            navigationContext = new(navigationContext.Parameters, CurrentEntry?.Path, newUrl);
            Navigate(newUrl, wbpRouterOptions.Value.Routes, routerHost.RouterViews, navigationContext);

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

    private void CallChildRouterViewNavigatedFromAction(object content, NavigationContext navigationContext){
        var childRouter = GetChildRouter(content);
        foreach (var routerView in childRouter){
            MvvmHelper.CallViewAndViewModelAction<INavigatedFromAware>(routerView.Content,
                navigatedFromAware => navigatedFromAware.OnNavigatedFrom(navigationContext));
            CallChildRouterViewNavigatedFromAction(routerView, navigationContext);
        }
    }

    private void Navigate(string targetUrl, IEnumerable<Route> routes, IEnumerable<RouterView> routerViews, NavigationContext navigationContext){
        string path = null;
        if (targetUrl == "/"){
            path = "";
        }
        else{
            var paths = targetUrl.Split("/", StringSplitOptions.RemoveEmptyEntries);
            path = paths[0];
        }


        Route targetRoute = null;
        foreach (var route in routes){
            if (route.Path.Trim('/') == path){
                targetRoute = route;
                break;
            }
        }

        List<RouterView> newRouterViews = new();
        foreach (var routerView in routerViews){
            if (targetRoute?.GetComponents().TryGetValue(routerView.RouterName, out var contentType) ?? false){
                //Has route


                if (routerView.Content?.GetType() == contentType){
                    //Refresh page.
                    MvvmHelper.CallViewAndViewModelAction<IRefreshAware>(routerView.Content,
                        refreshAware => refreshAware.OnRefresh(navigationContext));
                }
                else{
                    //Call navigated from.
                    MvvmHelper.CallViewAndViewModelAction<INavigatedFromAware>(routerView.Content,
                        navigatedFromAware => navigatedFromAware.OnNavigatedFrom(navigationContext));

                    CallChildRouterViewNavigatedFromAction(routerView.Content, navigationContext);

                    routerView.Content = serviceProvider.GetService(contentType);

                    //Call navigated to.
                    MvvmHelper.CallViewAndViewModelAction<INavigatedToAware>(routerView.Content,
                        navigatedToAware => navigatedToAware.OnNavigatedTo(navigationContext));
                }
            }
            else{
                //No route

                //Call navigated from.
                MvvmHelper.CallViewAndViewModelAction<INavigatedFromAware>(routerView.Content,
                    navigatedFromAware => navigatedFromAware.OnNavigatedFrom(navigationContext));
                CallChildRouterViewNavigatedFromAction(routerView.Content, navigationContext);

                routerView.Content = null;
            }

            newRouterViews.AddRange(GetChildRouter(routerView.Content));
        }

        var newPath = targetUrl.ReplaceFirst($"/{path}", "");
        var newRoutes = targetRoute?.Children;
        if (string.IsNullOrEmpty(newPath) || newRoutes?.Count == 0 || newRouterViews.Count == 0){
        }
        else{
            Navigate(newPath, newRoutes, newRouterViews, navigationContext);
        }
    }
}