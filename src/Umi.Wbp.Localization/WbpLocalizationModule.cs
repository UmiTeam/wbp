using Microsoft.Extensions.DependencyInjection;
using Umi.Wbp.Mvvm;
using Volo.Abp;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Umi.Wbp.Localization;

[DependsOn(typeof(WbpMvvmModule), typeof(AbpLocalizationModule))]
public class WbpLocalizationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
    }

    public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
    {
        WPFLocalizeExtension.Engine.LocalizeDictionary.Instance.DefaultProvider = context.ServiceProvider.GetRequiredService<WbpLocalizationProvider>();
    }
}