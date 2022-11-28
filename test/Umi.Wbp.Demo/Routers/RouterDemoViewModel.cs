using System;
using System.Windows;
using System.Windows.Input;
using PropertyChanged;
using Umi.Wbp.Commands;
using Umi.Wbp.Mvvm;
using Umi.Wbp.Routers;

namespace Umi.Wbp.Demo.Routers;

[AddINotifyPropertyChangedInterface]
public class RouterDemoViewModel : NavigationAwareAbstract, IViewModelFor<RouterDemo>
{
    private RouterDemo view;
    private readonly IRouterService routerService;

    public RouterDemoViewModel(IRouterService routerService){
        this.routerService = routerService;
        NavigateCommand = new RelayCommand(() => { routerService.Push("/router/test", new NavigationParameters() { { "Identifier", Guid.NewGuid() } }); });
    }

    RouterDemo IViewModelFor<RouterDemo>.View
    {
        get => view;
        set => view = value;
    }

    public ICommand NavigateCommand { get; set; }

    public override void OnNavigatedTo(NavigationContext navigationContext){
        MessageBox.Show("Navigated to router demo view model");
    }

    public override void OnNavigatedFrom(NavigationContext navigationContext){
        MessageBox.Show("Navigated from router demo view model");
    }
}