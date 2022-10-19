using System.Windows.Controls;
using Volo.Abp.DependencyInjection;

namespace Umi.Wbp.Test.Views;

public partial class TestDialog : UserControl, ITransientDependency
{
    public TestDialog()
    {
        InitializeComponent();
    }
}