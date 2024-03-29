﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Umi.Wbp.Commands;
using Umi.Wbp.Localization;
using Umi.Wbp.Mvvm;
using Umi.Wbp.Routers;

namespace Umi.Wbp.Demo;

public partial class HomeView : UserControl, IViewModelForSelf,INavigationAware
{
    private readonly IRouterService routerService;
    private readonly ILocalizationService localizationService;

    public HomeView(IRouterService routerService, ILocalizationService localizationService){
        this.routerService = routerService;
        this.localizationService = localizationService;
        InitializeComponent();
        ShowDialogDemoCommand = new RelayCommand(() => { routerService.Push("/dialog"); });
        ShowRouterDemoCommand = new RelayCommand(() => { routerService.Push("/router"); });
        ChangeCultureCommand = new RelayCommand(() => { localizationService.ChangeCultureInfo(new("en-US")); });
    }

    public ICommand ShowDialogDemoCommand { get; set; }
    public ICommand ShowRouterDemoCommand { get; set; }
    public ICommand ChangeCultureCommand { get; set; }

    public void OnNavigatedTo(NavigationContext navigationContext){
        MessageBox.Show("Navigated to home");
    }

    public void OnRefresh(NavigationContext navigationContext){
        
    }

    public void OnNavigatedFrom(NavigationContext navigationContext){
        MessageBox.Show("Navigated from home");
    }
}