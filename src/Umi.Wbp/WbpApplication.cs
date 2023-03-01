using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Threading;
using Autofac;
using Umi.Wbp.Mvvm;
using Umi.Wbp.Routers;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace Umi.Wbp;

public abstract class WbpApplication<TModule, TWindow> : System.Windows.Application where TModule : AbpModule where TWindow : Window, IRouterHost
{
    protected IAbpApplicationWithInternalServiceProvider AbpApplication { get; private set; }

    protected override async void OnStartup(StartupEventArgs e){
        BeforeWbpApplicationInitialize();
        var builder = new ContainerBuilder();
        builder.ComponentRegistryBuilder.Registered += (sender, args) => { args.ComponentRegistration.PipelineBuilding += (sender2, pipeline) => { pipeline.Use(new ViewAndViewModelResolveMiddleware()); }; };
        AbpApplication = await AbpApplicationFactory.CreateAsync(typeof(TModule), options =>
        {
            options.Services.AddAutofacServiceProviderFactory(builder);
            OnWbpApplicationInitialize(options);
        });

        AbpApplication.Services.AddSingleton(serviceProvider => serviceProvider.GetService(typeof(TWindow)) as IRouterHost);

        await AbpApplication.InitializeAsync();

        DispatcherUnhandledException += OnWbpApplicationError;

        AfterWbpApplicationInitialize();

        if (AbpApplication.Services.GetRequiredService(typeof(TWindow)) is Window mainWindow){
            MainWindow = mainWindow;
            MainWindow.Show();

            AbpApplication.Services.GetRequiredService<IRouterService>()?.Push("/");
        }
        else{
            throw new UserFriendlyException("Main component must be wpf window control");
        }
    }

    protected virtual void BeforeWbpApplicationInitialize(){
    }

    protected virtual void OnWbpApplicationError(object sender, DispatcherUnhandledExceptionEventArgs args){
    }

    protected virtual void AfterWbpApplicationInitialize(){
    }

    protected virtual void OnWbpApplicationInitialize(AbpApplicationCreationOptions options){
    }

    protected virtual void OnWbpApplicationExit(){
    }

    protected override async void OnExit(ExitEventArgs e){
        await AbpApplication.ShutdownAsync();
        OnWbpApplicationExit();
    }
}