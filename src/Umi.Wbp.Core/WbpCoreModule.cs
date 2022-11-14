using System;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Umi.Wbp.Core;

[DependsOn(typeof(AbpAutofacModule))]
public class WbpCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context){
    }
}