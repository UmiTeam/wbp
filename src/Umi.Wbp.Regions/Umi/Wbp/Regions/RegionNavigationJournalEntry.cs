using System;
using System.Windows;

namespace Umi.Wbp.Regions;

public class RegionNavigationJournalEntry : IRegionNavigationJournalEntry
{
    public RegionNavigationJournalEntry(Uri uri, NavigationParameters parameters, FrameworkElement view)
    {
        Uri = uri;
        Parameters = parameters;
        View = view;
    }

    public Uri Uri { get; }
    public NavigationParameters Parameters { get; }
    public FrameworkElement View { get; }
}