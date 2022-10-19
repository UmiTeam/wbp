using Umi.Wbp.Dialogs;
using Umi.Wbp.Events;
using Umi.Wbp.Regions;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Umi.Wbp;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(WbpDialogModule),
    typeof(WbpRegionModule),
    typeof(WbpEventModule))]
public class WbpModule : AbpModule
{
}