using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace WbpTutorial.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class WbpTutorialDbContextFactory : IDesignTimeDbContextFactory<WbpTutorialDbContext>
{
    public WbpTutorialDbContext CreateDbContext(string[] args)
    {
        WbpTutorialEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<WbpTutorialDbContext>()
            .UseSqlite(configuration.GetConnectionString("Default"));

        return new WbpTutorialDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../WbpTutorial.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
