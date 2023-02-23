using System.Windows.Controls;
using Umi.Wbp.Mvvm;

namespace WbpTutorial.Wpf.Views.Books;

public partial class CreateBookView : UserControl, IViewFor<CreateBookViewModel>
{
    public CreateBookView(){
        InitializeComponent();
    }

    public CreateBookViewModel ViewModel { get; set; }
}