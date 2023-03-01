using System.Collections.Generic;
using Umi.Wbp;
using Umi.Wbp.Message;
using Umi.Wbp.Routers;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using WbpTutorial.EntityFrameworkCore;
using WbpTutorial.Wpf.Views.Books;

namespace WbpTutorial.Wpf;

[DependsOn(
    typeof(WbpTutorialEntityFrameworkCoreModule),
    typeof(WbpTutorialApplicationModule),
    typeof(WbpModule), typeof(WbpMessageModule))]
public class WbpTutorialWpfModule : AbpModule
{
    private readonly ICollection<Route> routes = new List<Route>()
    {
        new()
        {
            Path = "/create-book",
            Component = typeof(CreateBookView)
        },
        new()
        {
            Path = "/list-book",
            Component = typeof(ListBookView)
        },
    };

    public override void PreConfigureServices(ServiceConfigurationContext context){
        PreConfigure<WbpRouterOptions>(options => { options.Routes = routes; });
    }

    public override void ConfigureServices(ServiceConfigurationContext context){
        Configure<WbpRouterOptions>(options =>
        {
            options.Routes = routes;
            options.BeforeEach = (context, next) => { next(context.To, true); };
        });

        Configure<AbpVirtualFileSystemOptions>(options => { options.FileSets.AddEmbedded<WbpTutorialWpfModule>(); });
    }
}