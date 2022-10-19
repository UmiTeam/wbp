using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Threading;

namespace Umi.Wbp.Test;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : WbpApplication
{
    protected override Type GetStartModuleType()
    {
        return typeof(TestModule);
    }

    protected override Window GetMainWindow()
    {
        return AbpApplication.Services.GetRequiredService<MainWindow>();
    }

    protected override void ExceptionHandler(object sender, DispatcherUnhandledExceptionEventArgs args)
    {
        base.ExceptionHandler(sender, args);
        MessageBox.Show(args.Exception.Message);
        args.Handled = true;
    }
}