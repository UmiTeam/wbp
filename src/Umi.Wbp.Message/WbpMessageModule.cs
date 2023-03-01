using Umi.Wbp.Localization;
using Volo.Abp.Modularity;

namespace Umi.Wbp.Message;

[DependsOn(typeof(WbpLocalizationModule))]
public class WbpMessageModule : AbpModule
{
}