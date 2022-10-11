using System.Windows.Controls;
using Umi.Wbp.Regions;
using Volo.Abp.DependencyInjection;

namespace Umi.Wbp.Test.Views;

public partial class TestView : UserControl, ITransientDependency
{
    public TestView(){
        InitializeComponent();
    }
}