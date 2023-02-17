using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Umi.Wbp.Core;

[DependsOn(typeof(AbpAutofacModule))]
public class WbpCoreModule : AbpModule
{
}