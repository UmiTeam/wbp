using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Umi.Wbp.Mvvm;

[DependsOn(typeof(AbpAutofacModule))]
public class WbpMvvmModule : AbpModule
{
}