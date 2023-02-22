using System.Collections.Generic;
using System.Windows;
using Umi.Wbp.Mvvm;
using Umi.Wbp.Routers;

namespace WbpTutorial.Wpf.Views;

public partial class MainWindow : Window, IRouterHost, IViewFor<MainWindowViewModel>
{
    public MainWindow(){
        InitializeComponent();
    }

    public ICollection<RouterView> RouterViews => new List<RouterView>() { RouterView };
    public MainWindowViewModel? ViewModel { get; set; }
}