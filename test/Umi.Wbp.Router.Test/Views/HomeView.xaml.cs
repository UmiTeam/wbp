using System.Windows.Controls;
using System.Windows.Input;
using Umi.Wbp.Commands;
using Umi.Wbp.Mvvm;
using Umi.Wbp.Routers;
using Volo.Abp.DependencyInjection;

namespace Umi.Wbp.Router.Test.Views;

public partial class HomeView : UserControl, ITransientDependency, IViewModelForSelf
{
    private readonly IRouterService routerService;

    public HomeView(IRouterService routerService){
        this.routerService = routerService;
        InitializeComponent();
        NavigateToNotExistPageCommand = new RelayCommand(() => { this.routerService.Push("/Not-Exist-Page"); });
    }

    public ICommand NavigateToNotExistPageCommand { get; set; }
}