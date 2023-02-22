using System.Threading.Tasks;

namespace WbpTutorial.Data;

public interface IWbpTutorialDbSchemaMigrator
{
    Task MigrateAsync();
}
