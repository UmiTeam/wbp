using System;

namespace Umi.Wbp.Regions;

public interface IRegionNavigationService
{
    /// <summary>
    /// Initiates navigation to the target specified by the <see cref="Uri"/>.
    /// </summary>
    /// <param name="target">The navigation target</param>
    /// <param name="navigationCallback">The callback executed when the navigation request is completed.</param>
    /// <remarks>
    /// Convenience overloads for this method can be found as extension methods on the 
    /// <see cref="NavigationAsyncExtensions"/> class.
    /// </remarks>
    void RequestNavigate(Uri target, Action<NavigationResult> navigationCallback);

    /// <summary>
    /// Initiates navigation to the target specified by the <see cref="Uri"/>.
    /// </summary>
    /// <param name="target">The navigation target</param>
    /// <param name="navigationCallback">The callback executed when the navigation request is completed.</param>
    /// <param name="navigationParameters">The navigation parameters specific to the navigation request.</param>
    /// <remarks>
    /// Convenience overloads for this method can be found as extension methods on the 
    /// <see cref="NavigationAsyncExtensions"/> class.
    /// </remarks>
    void RequestNavigate(Uri target, Action<NavigationResult> navigationCallback, NavigationParameters navigationParameters);

    /// <summary>
    /// Raised when the region is about to be navigated to content.
    /// </summary>
    event EventHandler<RegionNavigationEventArgs> Navigating;

    /// <summary>
    /// Raised when the region is navigated to content.
    /// </summary>
    event EventHandler<RegionNavigationEventArgs> Navigated;

    /// <summary>
    /// Raised when a navigation request fails.
    /// </summary>
    event EventHandler<RegionNavigationFailedEventArgs> NavigationFailed;
}