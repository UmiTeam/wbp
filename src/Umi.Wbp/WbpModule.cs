using Umi.Wbp.Application;
using Umi.Wbp.Core;
using Umi.Wbp.Dialogs;
using Umi.Wbp.Events;
using Umi.Wbp.Localization;
using Umi.Wbp.Routers;
using Volo.Abp.Modularity;

namespace Umi.Wbp;

[DependsOn(
    typeof(WbpDialogModule),
    typeof(WbpEventModule),
    typeof(WbpCoreModule),
    typeof(UmiWbpApplicationModule),
    typeof(WbpRouterModule),
    typeof(WbpLocalizationModule))]
public class WbpModule : AbpModule
{
}