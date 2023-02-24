using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Volo.Abp;
using WbpTutorial.Data;

namespace WbpTutorial.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void BeforeWbpApplicationInitialize(){
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Async(c => c.File("Logs/logs.txt"))
                .CreateLogger();
        }

        protected override void AfterWbpApplicationInitialize(){
            base.AfterWbpApplicationInitialize();
            var task = AbpApplication.ServiceProvider.GetRequiredService<WbpTutorialDbMigrationService>().MigrateAsync();
            task.Wait();
        }

        protected override void OnWbpApplicationInitialize(AbpApplicationCreationOptions options){
            options.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
        }

        protected override void OnWbpApplicationExit(){
            Log.CloseAndFlush();
        }
    }
}