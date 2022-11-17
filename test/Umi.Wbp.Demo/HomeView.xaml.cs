using System.Windows.Controls;
using System.Windows.Input;
using Umi.Wbp.Commands;
using Umi.Wbp.Localization;
using Umi.Wbp.Mvvm;
using Umi.Wbp.Routers;

namespace Umi.Wbp.Demo;

public partial class HomeView : UserControl, IViewModelForSelf
{
    private readonly IRouterService routerService;
    private readonly ILocalizationService localizationService;

    public HomeView(IRouterService routerService, ILocalizationService localizationService){
        this.routerService = routerService;
        this.localizationService = localizationService;
        InitializeComponent();
        ShowDialogDemoCommand = new RelayCommand(() => { routerService.Push("/dialog"); });
        ShowRouterDemoCommand = new RelayCommand(() => { routerService.Push("/router"); });
        ChangeCultureCommand = new RelayCommand(() => { localizationService.ChangeCultureInfo(new("zh-Hans")); });
    }

    public ICommand ShowDialogDemoCommand { get; set; }
    public ICommand ShowRouterDemoCommand { get; set; }
    public ICommand ChangeCultureCommand { get; set; }
}