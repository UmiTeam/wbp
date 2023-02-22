using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WbpTutorial.Data;
using Volo.Abp.DependencyInjection;

namespace WbpTutorial.EntityFrameworkCore;

public class EntityFrameworkCoreWbpTutorialDbSchemaMigrator
    : IWbpTutorialDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreWbpTutorialDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the WbpTutorialDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<WbpTutorialDbContext>()
            .Database
            .MigrateAsync();
    }
}
