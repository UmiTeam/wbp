using System.Windows;
using System.Windows.Controls;
using Umi.Wbp.Mvvm;
using Umi.Wbp.Routers;
using Volo.Abp.DependencyInjection;

namespace Umi.Wbp.Demo.Routers;

public partial class RouterDemo : UserControl, IViewFor<RouterDemoViewModel>, INavigationAware
{
    private RouterDemoViewModel viewModel;

    public RouterDemo(){
        InitializeComponent();
    }

    RouterDemoViewModel IViewFor<RouterDemoViewModel>.ViewModel
    {
        get => viewModel;
        set => viewModel = value;
    }

    public bool OnNavigatingTo(NavigationContext navigationContext){
        MessageBox.Show("Navigating to router demo view");
        return true;
    }

    public void OnNavigatedTo(NavigationContext navigationContext){
        MessageBox.Show("Navigated to router demo view");
    }

    public bool OnNavigatingFrom(NavigationContext navigationContext){
        MessageBox.Show("Navigating from router demo view");
        return true;
    }

    public void OnNavigatedFrom(NavigationContext navigationContext){
        MessageBox.Show("Navigated from router demo view");
    }
}