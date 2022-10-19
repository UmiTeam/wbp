using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Umi.Wbp.Mvvm;
using Umi.Wbp.Regions.Helpers;
using Volo.Abp;

namespace Umi.Wbp.Regions;

public class RegionControl : ContentControl, IRegion
{
    public static readonly Dictionary<string, RegionControl> RegionDictionary = new();
    private readonly Stack<IRegionNavigationJournalEntry> backStack = new();
    private readonly Stack<IRegionNavigationJournalEntry> forwardStack = new();


    public static readonly DependencyProperty RegionNameProperty = DependencyProperty.Register(
        nameof(RegionName), typeof(string), typeof(RegionControl),
        new PropertyMetadata("index", OnRegionNameChanged), ValidateRegionName);

    public string RegionName
    {
        get => (string)GetValue(RegionNameProperty);
        set => SetValue(RegionNameProperty, value);
    }

    private static void OnRegionNameChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
    {
        if (obj is RegionControl region && args.NewValue is string regionName)
        {
            RegionDictionary.TryAdd(regionName, region);
        }
    }

    public static bool ValidateRegionName(object value)
    {
        if (value is string regionName && !RegionDictionary.ContainsKey(regionName)) return true;
        return false;
    }

    public void RequestNavigate(Uri target, Action<NavigationResult> navigationCallback)
    {
        RequestNavigate(target, navigationCallback, new NavigationParameters());
    }

    public void RequestNavigate(Uri target, Action<NavigationResult> navigationCallback, NavigationParameters navigationParameters)
    {
        var uriParameter = UriParsingHelper.ParseQuery(target);
        foreach (var (key, value) in uriParameter)
        {
            navigationParameters.Add(key, value);
        }

        NavigationContext navigationContext = new(navigationParameters, target);
        RaiseNavigating(navigationContext);
        if (Views.TryGetValue(target, out var view))
        {
            if (CurrentEntry?.View is IConfirmNavigationRequest confirmNavigationRequest)
            {
                confirmNavigationRequest.ConfirmNavigationRequest(navigationContext, canNavigate => { RequestNavigate(target, view, navigationContext, navigationCallback); });
            }
            else
            {
                RequestNavigate(target, view, navigationContext, navigationCallback);
            }
        }
        else
        {
            navigationCallback?.Invoke(new NavigationResult(navigationContext, false));
            RaiseNavigationFailed(navigationContext, new BusinessException("No such uri view!"));
        }
    }

    public IDictionary<Uri, FrameworkElement> Views { get; } = new Dictionary<Uri, FrameworkElement>();

    public void Add(Uri uri, FrameworkElement view)
    {
        Views.TryAdd(uri, view);
    }

    public bool CanGoBack => backStack.Count > 0;

    public bool CanGoForward => forwardStack.Count > 0;
    public IRegionNavigationJournalEntry CurrentEntry { get; private set; }

    public void GoBack()
    {
        if (!CanGoBack) throw new BusinessException("Can not go back!");
        var journalEntry = backStack.Peek();
        var previousCurrentEntry = CurrentEntry;
        RequestNavigate(journalEntry.Uri, navigationResult =>
        {
            if (navigationResult.Result == true)
            {
                forwardStack.Push(previousCurrentEntry);
                backStack.Pop();
                backStack.Pop();
            }
        }, journalEntry.Parameters);
    }

    public void GoForward()
    {
        if (!CanGoForward) throw new BusinessException("Can not go forward!");
        var journalEntry = forwardStack.Peek();
        RequestNavigate(journalEntry.Uri, navigationResult =>
        {
            if (navigationResult.Result == true)
            {
                forwardStack.Pop();
            }
        }, journalEntry.Parameters);
    }

    public void RecordNavigation(IRegionNavigationJournalEntry entry)
    {
        if (CurrentEntry != null) backStack.Push(CurrentEntry);
        CurrentEntry = entry;
    }

    public void Clear()
    {
        backStack.Clear();
        forwardStack.Clear();
    }

    public event EventHandler<RegionNavigationEventArgs> Navigating;
    public event EventHandler<RegionNavigationEventArgs> Navigated;
    public event EventHandler<RegionNavigationFailedEventArgs> NavigationFailed;

    private void RaiseNavigating(NavigationContext navigationContext)
    {
        Navigating?.Invoke(this, new RegionNavigationEventArgs(navigationContext));
    }

    private void RaiseNavigated(NavigationContext navigationContext)
    {
        Navigated?.Invoke(this, new RegionNavigationEventArgs(navigationContext));
    }

    private void RaiseNavigationFailed(NavigationContext navigationContext, Exception error)
    {
        NavigationFailed?.Invoke(this, new RegionNavigationFailedEventArgs(navigationContext, error));
    }

    private void RequestNavigate(Uri uri, FrameworkElement view, NavigationContext navigationContext, Action<NavigationResult> navigationCallback)
    {
        if (!isNavigationTarget(view, navigationContext))
        {
            NotifyNavigationFailed(navigationContext, navigationCallback, new BusinessException("Is not navigation target view!"));
            return;
        }

        MvvmHelper.CallViewAndViewModelAction<INavigationAware>(CurrentEntry?.View, x => x.OnNavigatedFrom(navigationContext));

        Content = view;

        IRegionNavigationJournalEntry journalEntry = new RegionNavigationJournalEntry(uri, navigationContext.Parameters, view);
        RecordNavigation(journalEntry);

        MvvmHelper.CallViewAndViewModelAction<INavigationAware>(view, (x) => x.OnNavigatedTo(navigationContext));

        navigationCallback?.Invoke(new NavigationResult(navigationContext, true));

        RaiseNavigated(navigationContext);
    }

    private void NotifyNavigationFailed(NavigationContext navigationContext, Action<NavigationResult> navigationCallback, Exception e)
    {
        var navigationResult =
            e != null ? new NavigationResult(navigationContext, e) : new NavigationResult(navigationContext, false);

        navigationCallback?.Invoke(navigationResult);
        RaiseNavigationFailed(navigationContext, e);
    }

    private bool isNavigationTarget(FrameworkElement view, NavigationContext navigationContext)
    {
        var isNavigationTarget = true;
        if (view is INavigationAware viewNavigationAware)
        {
            if (!viewNavigationAware.IsNavigationTarget(navigationContext))
            {
                isNavigationTarget = false;
            }
        }

        if (view.DataContext is INavigationAware dataContextNavigationAware)
        {
            if (!dataContextNavigationAware.IsNavigationTarget(navigationContext))
            {
                isNavigationTarget = false;
            }
        }

        return isNavigationTarget;
    }
}