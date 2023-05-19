using System.Windows;
using System.Windows.Controls;
using Umi.Wbp.Routers;
using Volo.Abp.DependencyInjection;

namespace Wbp.Tutorial.Wpf.Views.User;

public partial class ProfileView : UserControl, ITransientDependency, INavigatedToAware
{
    public ProfileView(){
        InitializeComponent();
    }

    public void OnNavigatedTo(NavigationContext navigationContext){
        if (navigationContext.Parameters.TryGetValue("Para1", out string para)){
            MessageBox.Show(para);
        }
    }
}