using System;
using System.Windows;
using System.Windows.Input;
using PropertyChanged;
using Umi.Wbp.Commands;
using Umi.Wbp.Mvvm.Common;
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
        NavigateCommand = new RelayCommand(() => { routerService.Push("/router/test", new Parameters() { { "Identifier", Guid.NewGuid() } }); });
        NavigateBackCommand = new RelayCommand(() => { routerService.Push("/home"); });
    }

    RouterDemo IViewModelFor<RouterDemo>.View
    {
        get => view;
        set => view = value;
    }

    public ICommand NavigateCommand { get; set; }
    public ICommand NavigateBackCommand { get; set; }

    public override void OnNavigatedTo(NavigationContext navigationContext){
        MessageBox.Show("Navigated to router demo view model");
    }
}