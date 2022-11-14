using System;
using System.Windows;
using System.Windows.Input;
using PropertyChanged;
using Umi.Wbp.Commands;
using Umi.Wbp.Core.Common.Dialogs;
using Umi.Wbp.Dialogs;
using Umi.Wbp.Mvvm;
using Umi.Wbp.Routers;

namespace Umi.Wbp.Demo.Dialogs;

[AddINotifyPropertyChangedInterface]
public class DialogTestViewModel : IViewModelFor<DialogTest>
{
    private readonly IRouterService routerService;
    private readonly IDialogService dialogService;

    public DialogTestViewModel(IRouterService routerService, IDialogService dialogService){
        this.routerService = routerService;
        this.dialogService = dialogService;
        BackCommand = new RelayCommand(() => { routerService.GoBack(); });
        ShowOpenFileDialogCommand = new RelayCommand(() => { dialogService.ShowOpenFileDialog("All Files|*.*", result => { MessageBox.Show(result.Result == ButtonResult.Yes ? $"You have selected file in: {result.GetSelectedFilePath()}" : "You do not select a file"); }); });
        ChangeTitleCommand = new RelayCommand(() => { Title += $"[Modified][{Random.Shared.Next(100)}]"; });
    }

    public string Title { get; set; } = "This is dialog demo";
    public ICommand BackCommand { get; set; }
    public ICommand ShowOpenFileDialogCommand { get; set; }
    public ICommand ChangeTitleCommand { get; set; }

    private DialogTest view;

    DialogTest IViewModelFor<DialogTest>.View
    {
        get => view;
        set => view = value;
    }
}