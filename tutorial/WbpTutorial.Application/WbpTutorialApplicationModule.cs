using Umi.Wbp.Application;
using Volo.Abp.Modularity;
using WbpTutorial.Domain;

namespace WbpTutorial;

[DependsOn(typeof(UmiWbpApplicationModule), typeof(WbpTutorialDomainModule))]
public class WbpTutorialApplicationModule : AbpModule
{
}