using Volo.Abp.Modularity;
using WbpTutorial.Domain;

namespace WbpTutorial;

[DependsOn(typeof(WbpTutorialDomainModule))]
public class WbpTutorialApplicationModule : AbpModule
{
}