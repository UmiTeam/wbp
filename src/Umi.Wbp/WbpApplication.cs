using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using System;
using System.Windows;
using System.Windows.Threading;
using Autofac;
using Umi.Wbp.Core;
using Umi.Wbp.Routers;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace Umi.Wbp;

public abstract class WbpApplication<TModule, TWindow> : Application where TModule : AbpModule where TWindow : Window, IRouterHost
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

            var builder = new ContainerBuilder();
            builder.ComponentRegistryBuilder.Registered += (sender, args) => { args.ComponentRegistration.PipelineBuilding += (sender2, pipeline) => { pipeline.Use(new ViewAndViewModelResolveMiddleware()); }; };
            AbpApplication = await AbpApplicationFactory.CreateAsync(typeof(TModule), options =>
            {
                options.Services.AddAutofacServiceProviderFactory(builder);
                options.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
            });

            var options = AbpApplication.Services.ExecutePreConfiguredActions<WbpRouterOptions>();

            AbpApplication.Services.AddSingleton(serviceProvider => serviceProvider.GetService(typeof(TWindow)) as IRouterHost);

            await AbpApplication.InitializeAsync();

            DispatcherUnhandledException += OnAbpApplicationError;

            if (AbpApplication.Services.GetRequiredService(typeof(TWindow)) is Window mainWindow){
                MainWindow = mainWindow;
                MainWindow.Show();
            }
            else{
                throw new UserFriendlyException("Main component must be wpf window control");
            }
        }
        catch (Exception ex){
            Log.Fatal(ex, "Host terminated unexpectedly!");
        }
    }

    protected virtual void OnAbpApplicationError(object sender, DispatcherUnhandledExceptionEventArgs args){
    }

    protected virtual void OnAbpApplicationInitialized(){
    }

    protected override async void OnExit(ExitEventArgs e){
        await AbpApplication.ShutdownAsync();
        Log.CloseAndFlush();
    }
}