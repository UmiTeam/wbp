using Umi.Wbp.Helpers;
using Volo.Abp.Modularity;

namespace Umi.Wbp.Core;

public class WbpCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context){
        IocHelper.ServiceCollection = context.Services;
    }
}