using Volo.Abp.Modularity;

namespace Umi.Wbp.Mvvm.Test;

[DependsOn(typeof(WbpModule))]
public class MvvmTestModule : AbpModule
{
}