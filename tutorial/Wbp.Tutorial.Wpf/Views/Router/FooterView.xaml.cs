using System.Windows.Controls;
using Volo.Abp.DependencyInjection;

namespace Wbp.Tutorial.Wpf.Views.Router;

public partial class FooterView : UserControl,ITransientDependency
{
    public FooterView(){
        InitializeComponent();
    }
}