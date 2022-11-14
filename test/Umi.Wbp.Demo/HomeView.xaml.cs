using System.Windows.Controls;
using System.Windows.Input;
using Umi.Wbp.Commands;
using Umi.Wbp.Mvvm;
using Umi.Wbp.Routers;

namespace Umi.Wbp.Demo;

public partial class HomeView : UserControl, IViewModelForSelf
{
    private readonly IRouterService routerService;

    public HomeView(IRouterService routerService){
        this.routerService = routerService;
        InitializeComponent();
        ShowDialogDemoCommand = new RelayCommand(() => { routerService.Push("/dialog"); });
        ShowRouterDemoCommand = new RelayCommand(() => { routerService.Push("/router"); });
    }

    public ICommand ShowDialogDemoCommand { get; set; }
    public ICommand ShowRouterDemoCommand { get; set; }
}