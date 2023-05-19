using System.Windows;
using System.Windows.Controls;
using Umi.Wbp.Mvvm.Common;
using Umi.Wbp.Routers;
using Volo.Abp.DependencyInjection;

namespace Wbp.Tutorial.Wpf.Views.User;

public partial class UserView : UserControl, IScopedDependency
{
    private readonly IRouterService routerService;

    public UserView(IRouterService routerService){
        this.routerService = routerService;
        InitializeComponent();
    }

    private void GoProfile(object sender, RoutedEventArgs e){
        routerService.Push("/user/profile", new Parameters() { { "Para1", "This is parameter 1" } });
    }

    private void GoPost(object sender, RoutedEventArgs e){
        routerService.Push("/user/post");
    }
}