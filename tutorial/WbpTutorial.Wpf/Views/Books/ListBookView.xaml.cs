using System.Windows.Controls;
using Umi.Wbp.Mvvm;

namespace WbpTutorial.Wpf.Views.Books;

public partial class ListBookView : UserControl, IViewFor<ListBookViewModel>
{
    public ListBookView(){
        InitializeComponent();
    }

    public ListBookViewModel ViewModel { get; set; }
}