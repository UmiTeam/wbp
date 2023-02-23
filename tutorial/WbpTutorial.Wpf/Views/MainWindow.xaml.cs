using System.Collections.Generic;
using System.Windows;
using Umi.Wbp.Mvvm;
using Umi.Wbp.Routers;

namespace WbpTutorial.Wpf.Views;

public partial class MainWindow : Window, IRouterHost, IViewFor<MainWindowViewModel>
{
    private readonly IRouterService routerService;

    public MainWindow(IRouterService routerService){
        this.routerService = routerService;
        InitializeComponent();
    }

    public ICollection<RouterView> RouterViews => new List<RouterView>() { RouterView };
    public MainWindowViewModel ViewModel { get; set; }

    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e){
        routerService.Push("/create-book");
    }
}