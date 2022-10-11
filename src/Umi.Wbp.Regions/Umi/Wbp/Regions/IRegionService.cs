using System;
using System.Windows;

namespace Umi.Wbp.Regions;

public interface IRegionService
{
    void RequestNavigate<T>(string regionName, Action<NavigationResult> navigationCallback = null, NavigationParameters navigationParameters = null) where T : FrameworkElement;
}