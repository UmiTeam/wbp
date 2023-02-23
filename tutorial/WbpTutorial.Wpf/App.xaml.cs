using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using WbpTutorial.Data;

namespace WbpTutorial.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnAbpApplicationInitialized(){
            base.OnAbpApplicationInitialized();
            var task = AbpApplication.ServiceProvider.GetRequiredService<WbpTutorialDbMigrationService>().MigrateAsync();
            task.Wait();
        }
    }
}