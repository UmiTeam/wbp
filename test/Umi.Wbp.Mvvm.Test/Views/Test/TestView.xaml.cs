using System.Windows.Controls;

namespace Umi.Wbp.Mvvm.Test.Views.Test;

public partial class TestView : UserControl, IViewFor<TestViewModel>
{

    public TestView(){
        InitializeComponent();
    }

    // /// <summary>
    // /// Use public implement if you want other object to access the view model.
    // /// </summary>
    // public TestViewModel ViewModel { get; set; }

    private TestViewModel viewModel;
    /// <summary>
    /// Use explicit implement with backing field if you don't want other object to access the view model.
    /// </summary>
    TestViewModel IViewFor<TestViewModel>.ViewModel
    {
        get => viewModel;
        set => viewModel = value;
    }
}