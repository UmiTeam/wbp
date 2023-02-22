using Umi.Wbp;
using Volo.Abp.Modularity;
using WbpTutorial.EntityFrameworkCore;

namespace WbpTutorial.Wpf;

[DependsOn(typeof(WbpTutorialEntityFrameworkCoreModule), typeof(WbpModule))]
public class WbpTutorialWpfModule : AbpModule
{
}