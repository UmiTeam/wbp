using System.Windows;
using Umi.Wbp.Dialogs;
using Volo.Abp.DependencyInjection;

namespace Umi.Wbp.Test;

public partial class TestDialogWindow : Window, IDialogWindow, ITransientDependency
{
    public TestDialogWindow(){
        InitializeComponent();
    }

    public IDialogResult Result { get; set; }
}