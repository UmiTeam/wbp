using System.Collections.Generic;
using System.Windows;
using Umi.Wbp.Router.Test.Views;
using Umi.Wbp.Routers;
using Volo.Abp.Modularity;

namespace Umi.Wbp.Router.Test;

[DependsOn(typeof(WbpModule))]
public class RouterTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context){
        Configure<WbpRouterOptions>(options =>
        {
            options.Routes = new List<Route>()
            {
                new Route()
                {
                    Path = "/",
                    Component = typeof(HomeView)
                }
            };
            options.BeforeEach = (context, next) =>
            {
                MessageBox.Show("Can't navigate");
                // next(context.To, true);
            };
            options.AfterEach = (context) => { MessageBox.Show(context.To); };
        });
    }
}