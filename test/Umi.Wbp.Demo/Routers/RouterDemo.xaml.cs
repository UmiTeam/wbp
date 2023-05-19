using System.Windows;
using System.Windows.Controls;
using Umi.Wbp.Mvvm;
using Umi.Wbp.Routers;

namespace Umi.Wbp.Demo.Routers;

public partial class RouterDemo : UserControl, IViewFor<RouterDemoViewModel>, INavigationAware
{
    public RouterDemo(){
        InitializeComponent();
    }

    RouterDemoViewModel IViewFor<RouterDemoViewModel>.ViewModel { get; set; }

    public void OnNavigatedTo(NavigationContext navigationContext){
        MessageBox.Show("Navigated to router demo view");
    }

    public void OnRefresh(NavigationContext navigationContext){
        MessageBox.Show("Refresh router demo view");
    }

    public void OnNavigatedFrom(NavigationContext navigationContext){
        MessageBox.Show("Navigated from router demo view");
    }
}