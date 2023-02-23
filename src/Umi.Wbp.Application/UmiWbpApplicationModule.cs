using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Umi.Wbp.Application;

[DependsOn(typeof(AbpDddApplicationModule))]
public class UmiWbpApplicationModule : AbpModule
{
}