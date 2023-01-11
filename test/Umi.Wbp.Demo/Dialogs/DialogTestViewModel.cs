using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.Localization;
using PropertyChanged;
using Umi.Wbp.Commands;
using Umi.Wbp.Core.Common.Dialogs;
using Umi.Wbp.Demo.Localization;
using Umi.Wbp.Dialogs;
using Umi.Wbp.Mvvm;
using Umi.Wbp.Routers;

namespace Umi.Wbp.Demo.Dialogs;

[AddINotifyPropertyChangedInterface]
public class DialogTestViewModel : IViewModelFor<DialogTest>
{
    private readonly IRouterService routerService;
    private readonly IDialogService dialogService;
    private readonly IStringLocalizer<DemoResource> stringLocalizer;

    public DialogTestViewModel(IRouterService routerService, IDialogService dialogService, IStringLocalizer<DemoResource> stringLocalizer){
        this.routerService = routerService;
        this.dialogService = dialogService;
        this.stringLocalizer = stringLocalizer;
        ShowOpenFileDialogCommand = new RelayCommand(() => { dialogService.ShowOpenFileDialog("All Files|*.*", result => { MessageBox.Show(result.Result == ButtonResult.Yes ? $"You have selected file in: {result.GetSelectedFilePath()}" : "You do not select a file"); }); });
        ChangeTitleCommand = new RelayCommand(() => { Title += $"[Modified][{Random.Shared.Next(100)}]"; });
    }

    public string Title { get; set; } = "This is dialog demo";
    public ICommand ShowOpenFileDialogCommand { get; set; }
    public ICommand ChangeTitleCommand { get; set; }

    private DialogTest view;

    DialogTest IViewModelFor<DialogTest>.View
    {
        get => view;
        set => view = value;
    }
}