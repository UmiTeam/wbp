using System;
using System.Collections.Generic;
using System.Windows;

namespace Umi.Wbp.Regions;

public interface IRegion : IRegionNavigationService, IRegionNavigationJournal
{
    public IDictionary<Uri, FrameworkElement> Views { get; }
    string Name { get; set; }
    void Add(Uri uri, FrameworkElement view);
}