using System.Windows.Controls;
using Volo.Abp.DependencyInjection;

namespace Wbp.Tutorial.Wpf.Views.User;

public partial class PostView : UserControl,ITransientDependency
{
    public PostView(){
        InitializeComponent();
    }
}