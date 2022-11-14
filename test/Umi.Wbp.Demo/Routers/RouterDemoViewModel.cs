using System.Windows;
using PropertyChanged;
using Umi.Wbp.Mvvm;
using Umi.Wbp.Routers;

namespace Umi.Wbp.Demo.Routers;

[AddINotifyPropertyChangedInterface]
public class RouterDemoViewModel : NavigationAwareAbstract, IViewModelFor<RouterDemo>
{
    private RouterDemo view;

    RouterDemo IViewModelFor<RouterDemo>.View
    {
        get => view;
        set => view = value;
    }

    public override bool OnNavigatingTo(NavigationContext navigationContext){
        MessageBox.Show("Navigating to router demo view model");
        return base.OnNavigatingTo(navigationContext);
    }

    public override void OnNavigatedTo(NavigationContext navigationContext){
        MessageBox.Show("Navigated to router demo view model");
    }

    public override bool OnNavigatingFrom(NavigationContext navigationContext){
        MessageBox.Show("Navigating from router demo view model");
        return base.OnNavigatingFrom(navigationContext);
    }

    public override void OnNavigatedFrom(NavigationContext navigationContext){
        MessageBox.Show("Navigated from router demo view model");
    }
}