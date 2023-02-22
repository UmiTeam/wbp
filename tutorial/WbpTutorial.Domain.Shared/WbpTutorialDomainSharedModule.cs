using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.Threading;
using Volo.Abp.VirtualFileSystem;
using WbpTutorial.Localization;

namespace WbpTutorial;

[DependsOn(typeof(AbpThreadingModule))]
public class WbpTutorialDomainSharedModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context){
        WbpTutorialGlobalFeatureConfigurator.Configure();
        WbpTutorialModuleExtensionConfigurator.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context){
        Configure<AbpVirtualFileSystemOptions>(options => { options.FileSets.AddEmbedded<WbpTutorialDomainSharedModule>(); });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<WbpTutorialResource>("en")
                // .AddBaseTypes(typeof(AbpValidationResource))
                .AddVirtualJson("/Localization/WbpTutorial");

            options.DefaultResourceType = typeof(WbpTutorialResource);
        });

        Configure<AbpExceptionLocalizationOptions>(options => { options.MapCodeNamespace("WbpTutorial", typeof(WbpTutorialResource)); });
    }
}