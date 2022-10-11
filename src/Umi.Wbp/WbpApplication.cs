using System;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Volo.Abp;

namespace Umi.Wbp;

public abstract class WbpApplication : Application
{
    protected IAbpApplicationWithInternalServiceProvider AbpApplication { get; private set; }

    protected override async void OnStartup(StartupEventArgs e){
        ShutdownMode = ShutdownMode.OnMainWindowClose;
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

        try{
            Log.Information("Starting WPF host.");

            AbpApplication = await AbpApplicationFactory.CreateAsync(GetStartModuleType(), options =>
            {
                options.UseAutofac();
                options.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
            });

            await AbpApplication.InitializeAsync();

            DispatcherUnhandledException += ExceptionHandler;

            MainWindow = GetMainWindow();
            MainWindow.Show();
        }
        catch (Exception ex){
            Log.Fatal(ex, "Host terminated unexpectedly!");
        }
    }

    protected abstract Type GetStartModuleType();

    protected abstract Window GetMainWindow();

    protected virtual void ExceptionHandler(object sender, DispatcherUnhandledExceptionEventArgs args){
        Log.Error(args.Exception, "Unhandled exception!");
    }

    protected override async void OnExit(ExitEventArgs e){
        await AbpApplication.ShutdownAsync();
        Log.CloseAndFlush();
    }
}