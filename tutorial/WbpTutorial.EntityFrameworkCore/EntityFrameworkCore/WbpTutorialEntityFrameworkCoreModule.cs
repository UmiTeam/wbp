using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;
using WbpTutorial.Domain;

namespace WbpTutorial.EntityFrameworkCore;

[DependsOn(typeof(AbpEntityFrameworkCoreModule), typeof(WbpTutorialDomainModule))]
public class WbpTutorialEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context){
        WbpTutorialEfCoreEntityExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context){
        context.Services.AddAbpDbContext<WbpTutorialDbContext>(options =>
        {
            /* Remove "includeAllEntities: true" to create
             * default repositories only for aggregate roots */
            options.AddDefaultRepositories(includeAllEntities: true);
        });

        Configure<AbpDbContextOptions>(options =>
        {
            /* The main point to change your DBMS.
             * See also WbpTutorialMigrationsDbContextFactory for EF Core tooling. */
            options.UseSqlite();
        });

        Configure<AbpUnitOfWorkDefaultOptions>(options => { options.TransactionBehavior = UnitOfWorkTransactionBehavior.Disabled; });
    }
}