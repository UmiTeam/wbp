﻿using System.Windows;
using System.Windows.Controls;
using PropertyChanged;
using Umi.Wbp.Mvvm;
using Umi.Wbp.Routers;

namespace Routers;

[AddINotifyPropertyChangedInterface]
public partial class RouterDemoHeader : UserControl, IViewModelForSelf, INavigationAware
{
    public RouterDemoHeader(){
        InitializeComponent();
    }

    public void OnNavigatedTo(NavigationContext navigationContext){
        MessageBox.Show("Navigated to header");
    }

    public void OnRefresh(NavigationContext navigationContext){
        MessageBox.Show("Refresh to header");
    }

    public void OnNavigatedFrom(NavigationContext navigationContext){
        MessageBox.Show("Navigated from header");
    }
}