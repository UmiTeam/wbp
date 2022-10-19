using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Umi.Wbp.Test;

[DependsOn(typeof(AbpAutofacModule),
    typeof(WbpModule))]
public class TestModule : AbpModule
{
    //public override void ConfigureServices(ServiceConfigurationContext context){
    //    context.Services.AddSingleton<MainWindow>();
    //}
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
    }
}