using System;
using System.Threading;
using System.Windows;
using Umi.Wbp.Core.Common.Dialogs;
using Umi.Wbp.Dialogs;
using Umi.Wbp.Regions;
using Umi.Wbp.Test.Views;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Umi.Wbp.Test;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, ITransientDependency
{
    private readonly HelloWorldService _helloWorldService;
    private readonly IDialogService dialogService;
    private readonly IRegionService regionService;

    public MainWindow(HelloWorldService helloWorldService, IDialogService dialogService, IRegionService regionService)
    {
        _helloWorldService = helloWorldService;
        this.dialogService = dialogService;
        this.regionService = regionService;
        InitializeComponent();
    }

    protected override void OnContentRendered(EventArgs e)
    {
        HelloLabel.Content = _helloWorldService.SayHello();
    }

    private void ShowDialog_OnClick(object sender, RoutedEventArgs e)
    {
        dialogService.ShowDialog<TestDialog>();
    }

    private void ShowDialogWithCustomWindow_OnClick(object sender, RoutedEventArgs e)
    {
        dialogService.ShowDialog<TestDialog>(null, null, nameof(TestDialogWindow));
    }

    private void ShowWithCustomWindow_OnClick(object sender, RoutedEventArgs e)
    {
        dialogService.Show<TestDialog>(null, null, nameof(TestDialogWindow));
    }

    private void RequestNavigation_OnClick(object sender, RoutedEventArgs e)
    {
        regionService.RequestNavigate<TestView>("TestRegion", navigationParameters: new NavigationParameters() { { "Identifier", Random.Shared.Next(10, 100).ToString() } });
    }

    private void RequestNavigationBack_OnClick(object sender, RoutedEventArgs e)
    {
        RegionControl.RegionDictionary["TestRegion"].GoBack();
    }

    private void RequestNavigationForward_OnClick(object sender, RoutedEventArgs e)
    {
        RegionControl.RegionDictionary["TestRegion"].GoForward();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        // var result = dialogService.ShowOpenFileDialog("");
        // var temp1 = result.GetSelectedFilePath();
        // var temp2 = result.GetSelectedFilePaths();
        // var result = dialogService.ShowFolderBrowserDialog("SAASAS");
        // var temp1 = result.GetSelectedFolderPath();
        // var temp2 = result.GetSelectedFolderPaths();
        // _ = "";
        dialogService.ShowProgressDialog("WTF!!!", (dialog, args) =>
        {
            for (int i = 0; i < 5; i++)
            {
                if (dialog.CancellationPending)
                {
                    args.Cancel = true;
                    return;
                }

                if (i == 4)
                {
                    throw new BusinessException("SSS");
                }

                dialog.ReportProgress(i * 2);
                Thread.Sleep(TimeSpan.FromSeconds(i));
            }
        });
    }
}