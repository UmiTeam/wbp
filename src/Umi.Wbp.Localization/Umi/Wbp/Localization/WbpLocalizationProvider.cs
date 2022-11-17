using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Localization;
using WPFLocalizeExtension.Providers;

namespace Umi.Wbp.Localization;

public class WbpLocalizationProvider : ILocalizationProvider, ITransientDependency
{
    private readonly IStringLocalizer stringLocalizer;
    private readonly IOptions<WbpLocalizationOptions> wbpLocalizationOptions;
    private readonly IOptions<AbpLocalizationOptions> abpLocalizationOptions;
    private readonly IAbpLazyServiceProvider lazyServiceProvider;

    public WbpLocalizationProvider(IOptions<WbpLocalizationOptions> wbpLocalizationOptions, IAbpLazyServiceProvider lazyServiceProvider, IOptions<AbpLocalizationOptions> abpLocalizationOptions)
    {
        this.wbpLocalizationOptions = wbpLocalizationOptions;
        this.lazyServiceProvider = lazyServiceProvider;
        this.abpLocalizationOptions = abpLocalizationOptions;
        var stringLocalizerFactory= lazyServiceProvider.LazyGetRequiredService<IStringLocalizerFactory>();
        stringLocalizer = wbpLocalizationOptions.Value.LocalizationResource != null ? stringLocalizerFactory.Create(wbpLocalizationOptions.Value.LocalizationResource) : stringLocalizerFactory.CreateDefaultOrNull();
        if (stringLocalizer == null)
        {
            throw new AbpException($"Set {nameof(LocalizationResource)} or define the default localization resource type (by configuring the {nameof(AbpLocalizationOptions)}.{nameof(AbpLocalizationOptions.DefaultResourceType)}) to be able to use the WBP localization!");
        }
        AvailableCultures = new();
        foreach (var languageInfo in abpLocalizationOptions.Value.Languages)
        {
            AvailableCultures.Add(new CultureInfo(languageInfo.CultureName));
        }
    }

    public FullyQualifiedResourceKeyBase GetFullyQualifiedResourceKey(string key, DependencyObject target)
    {
        return new FQAssemblyDictionaryKey(key);
    }

    public object GetLocalizedObject(string key, DependencyObject target, CultureInfo culture)
    {
        if (key == null) return null;
        var loc = stringLocalizer.GetString(key);
        return loc.Value;
    }

    public ObservableCollection<CultureInfo> AvailableCultures { get; }
    public event ProviderChangedEventHandler ProviderChanged;
    public event ProviderErrorEventHandler ProviderError;
    public event ValueChangedEventHandler ValueChanged;
}