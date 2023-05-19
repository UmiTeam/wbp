using System.Windows.Forms;
using Umi.Wbp.Mvvm;
using Volo.Abp.DependencyInjection;
using UserControl = System.Windows.Controls.UserControl;

namespace Umi.Wbp.Demo.Dialogs;

public partial class DialogTest : UserControl, IViewFor<DialogTestViewModel>
{
    public DialogTest(){
        InitializeComponent();
    }

    DialogTestViewModel IViewFor<DialogTestViewModel>.ViewModel { get; set; }
}