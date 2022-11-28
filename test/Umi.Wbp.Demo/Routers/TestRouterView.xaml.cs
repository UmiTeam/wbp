using System;
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
    public bool OnNavigatingTo(NavigationContext navigationContext){
        return true;
    }

    public void OnNavigatedTo(NavigationContext navigationContext){
        if (navigationContext.Parameters.TryGetValue("Identifier", out Guid identifier)){
            HelloWorldString = identifier.ToString();
        }
    }

    public bool OnNavigatingFrom(NavigationContext navigationContext){
        return true;
    }

    public void OnNavigatedFrom(NavigationContext navigationContext){
    }
}