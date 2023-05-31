using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Umi.Wbp.Mvvm;
using Volo.Abp.Modularity;

namespace Umi.Wbp.Routers;

[DependsOn(typeof(WbpMvvmModule))]
public class WbpRouterModule : AbpModule
{
    // public override void ConfigureServices(ServiceConfigurationContext context){
    //     var options = context.Services.ExecutePreConfiguredActions<WbpRouterOptions>();
    //     if (options.Routes != null)
    //         RegisterRouteComponent(options.Routes, context.Services);
    // }
    //
    // private void RegisterRouteComponent(IEnumerable<Route> routes, IServiceCollection serviceCollection){
    //     foreach (var route in routes){
    //         foreach (var type in route.GetComponents().Values){
    //             // Register view as transient.
    //             serviceCollection.AddTransient(type);
    //
    //             // Register view model as transient.
    //             foreach (var @interface in type.GetInterfaces()){
    //                 if (@interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof(IViewFor<>)){
    //                     serviceCollection.AddTransient(@interface.GetGenericArguments()[0]);
    //                 }
    //             }
    //         }
    //
    //         if (route.Children.Count > 0) RegisterRouteComponent(route.Children, serviceCollection);
    //     }
    // }
}