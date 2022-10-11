using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Umi.Wbp.Regions;

public class RegionService : IRegionService, ITransientDependency
{
    private readonly IServiceProvider serviceProvider;

    public RegionService(IServiceProvider serviceProvider){
        this.serviceProvider = serviceProvider;
    }

    public void RequestNavigate<T>(string regionName, Action<NavigationResult> navigationCallback = null, NavigationParameters navigationParameters = null) where T : FrameworkElement{
        if (RegionControl.RegionDictionary.TryGetValue(regionName, out var region)){
            var view = serviceProvider.GetRequiredService<T>();
            Uri uri = new("http://localhost/"+Random.Shared.NextInt64(),UriKind.Absolute);
            region.Add(uri, view);
            region.RequestNavigate(uri, navigationCallback, navigationParameters);
        }
        else{
            throw new BusinessException("No such region!");
        }
    }
}