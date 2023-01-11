using System;
using System.Windows;
using System.Windows.Controls;
using PropertyChanged;
using Umi.Wbp.Mvvm;
using Umi.Wbp.Routers;

namespace Routers;

[AddINotifyPropertyChangedInterface]
public partial class TestRouterView : UserControl, IViewModelForSelf, INavigationAware
{
    public TestRouterView(){
        InitializeComponent();
    }

    public string HelloWorldString { get; set; }

    public void OnNavigatedTo(NavigationContext navigationContext){
        if (navigationContext.Parameters.TryGetValue("Identifier", out Guid identifier)){
            HelloWorldString = identifier.ToString();
        }
        MessageBox.Show("Navigated to body");
    }

    public void OnRefresh(NavigationContext navigationContext){
        MessageBox.Show("Refresh body");
    }
}