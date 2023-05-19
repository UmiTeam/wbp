using System.Windows;
using System.Windows.Controls;
using Umi.Wbp.Routers;
using Volo.Abp.DependencyInjection;

namespace Wbp.Tutorial.Wpf.Views.Router;

public partial class ContentView : UserControl, ITransientDependency
{
    private readonly IRouterService routerService;

    public ContentView(IRouterService routerService){
        this.routerService = routerService;
        InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e){
        routerService.Push("/home");
    }

    private void ButtonBase_OnClick2(object sender, RoutedEventArgs e){
        routerService.Push("/user");
    }
}