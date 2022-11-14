using System.Collections.Generic;
using Umi.Wbp.Demo.Dialogs;
using Umi.Wbp.Demo.Routers;
using Umi.Wbp.Routers;
using Volo.Abp.Modularity;

namespace Umi.Wbp.Demo;

[DependsOn(typeof(WbpModule))]
public class DemoModule : AbpModule
{
    private readonly ICollection<Route> routes = new List<Route>()
    {
        new()
        {
            Path = "/home",
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
            Component = typeof(RouterDemo)
        }
    };

    public override void PreConfigureServices(ServiceConfigurationContext context){
        PreConfigure<WbpRouterOptions>(options => { options.Routes = routes; });
    }

    public override void ConfigureServices(ServiceConfigurationContext context){
        Configure<WbpRouterOptions>(options => { options.Routes = routes; });
    }
}