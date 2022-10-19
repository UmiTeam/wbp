using Volo.Abp.Modularity;

namespace Umi.Wbp.Core;

public class WbpCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        IocHelper.Services = context.Services;
    }
}