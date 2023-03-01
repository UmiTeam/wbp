using System;
using System.Collections.Generic;
using System.Windows;
using Routers;
using Umi.Wbp.Demo.Dialogs;
using Umi.Wbp.Demo.Localization;
using Umi.Wbp.Demo.Routers;
using Umi.Wbp.Localization;
using Umi.Wbp.Message;
using Umi.Wbp.Routers;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Umi.Wbp.Demo;

[DependsOn(typeof(WbpModule), typeof(WbpMessageModule))]
public class DemoModule : AbpModule
{
    private readonly ICollection<Route> routes = new List<Route>()
    {
        new()
        {
            Path = "/",
            Component = typeof(HomeView)
        },
        new()
        {
            Path = "/dialog",
            Component = typeof(DialogTest)
        },
        new()
        {
            Path = "/router",
            Component = typeof(RouterDemo),
            Children = new List<Route>
            {
                new()
                {
                    Path = "/test",
                    Components = new Dictionary<string, Type>()
                    {
                        { "Header", typeof(RouterDemoHeader) },
                        { "Body", typeof(TestRouterView) },
                        { "Footer", typeof(RouterDemoFooter) }
                    }
                }
            }
        }
    };

    public override void PreConfigureServices(ServiceConfigurationContext context){
        PreConfigure<WbpRouterOptions>(options => { options.Routes = routes; });
    }

    public override void ConfigureServices(ServiceConfigurationContext context){
        Configure<WbpRouterOptions>(options =>
        {
            options.Routes = routes;
            options.BeforeEach = (context, next) =>
            {
                // MessageBox.Show($"From:[{context.From}]-To:[{context.To}]");
                next(context.To, true);
            };
        });
        Configure<WbpLocalizationOptions>(options => { options.LocalizationResource = typeof(DemoResource); });

        Configure<AbpVirtualFileSystemOptions>(options => { options.FileSets.AddEmbedded<DemoModule>(); });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<DemoResource>("en")
                .AddVirtualJson("/Localization/Demo");

            options.DefaultResourceType = typeof(DemoResource);
        });

        Configure<AbpExceptionLocalizationOptions>(options => { options.MapCodeNamespace("Demo", typeof(DemoResource)); });
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Languages.Add(new LanguageInfo("en", "en", "English"));
            options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
        });
    }
}