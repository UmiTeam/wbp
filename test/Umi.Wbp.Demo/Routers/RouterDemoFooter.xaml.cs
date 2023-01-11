using System.Windows;
using System.Windows.Controls;
using PropertyChanged;
using Umi.Wbp.Mvvm;
using Umi.Wbp.Routers;

namespace Routers;

[AddINotifyPropertyChangedInterface]
public partial class RouterDemoFooter : UserControl, IViewModelForSelf, INavigationAware
{
    public RouterDemoFooter(){
        InitializeComponent();
    }

    public void OnNavigatedTo(NavigationContext navigationContext){
        MessageBox.Show("Navigated to footer");
    }

    public void OnRefresh(NavigationContext navigationContext){
        MessageBox.Show("Refresh to footer");
    }
}