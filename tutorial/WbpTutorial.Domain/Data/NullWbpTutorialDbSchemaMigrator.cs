using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace WbpTutorial.Data;

/* This is used if database provider does't define
 * IWbpTutorialDbSchemaMigrator implementation.
 */
public class NullWbpTutorialDbSchemaMigrator : IWbpTutorialDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
