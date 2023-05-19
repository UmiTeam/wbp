using System;
using System.Collections.Generic;
using System.Windows;
using Umi.Wbp;
using Umi.Wbp.Routers;
using Volo.Abp.Modularity;
using Wbp.Tutorial.Wpf.Views.Router;
using Wbp.Tutorial.Wpf.Views.User;

namespace Wbp.Tutorial.Wpf;

[DependsOn(typeof(WbpModule))]
public class TutorialModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context){
        Configure<WbpRouterOptions>(options =>
        {
            options.Routes = new List<Route>()
            {
                new()
                {
                    Path = "/",
                    Components = new()
                    {
                        { "HeaderRouter", typeof(HeaderView) },
                        { "ContentRouter", typeof(ContentView) },
                        { "FooterRouter", typeof(FooterView) }
                    }
                },
                new()
                {
                    Path = "/home",
                    Components = new()
                    {
                        { "ContentRouter", typeof(HomeView) }
                    }
                },
                new()
                {
                    Path = "/user",
                    Components = new()
                    {
                        { "ContentRouter", typeof(UserView) }
                    },
                    Children = new List<Route>()
                    {
                        new()
                        {
                            Path = "/profile",
                            Component = typeof(ProfileView)
                        },
                        new()
                        {
                            Path = "/post",
                            Component = typeof(PostView)
                        }
                    }
                },
            };
            
            options.BeforeEach=(context, next) =>
            {
                next(context.To, true);
            };

            options.AfterEach = (context) =>
            {
                MessageBox.Show($"Navigate to {context.To} success");
            };
        });
    }
}