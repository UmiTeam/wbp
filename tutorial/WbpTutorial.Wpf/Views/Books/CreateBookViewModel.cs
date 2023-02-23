using System.Threading.Tasks;
using System.Windows.Input;
using PropertyChanged;
using Umi.Wbp.Commands;
using WbpTutorial.Books;
using WbpTutorial.Domain.Books;

namespace WbpTutorial.Wpf.Views.Books;

[AddINotifyPropertyChangedInterface]
public class CreateBookViewModel
{
    private readonly IBookAppService bookAppService;

    public CreateBookViewModel(IBookAppService bookAppService){
        this.bookAppService = bookAppService;
        CreateBookCommand = new AsyncRelayCommand(CreateBookAsync);
    }

    public Book Book { get; set; } = new();
    public ICommand CreateBookCommand { get; set; }

    private async Task CreateBookAsync(){
        await bookAppService.CreateAsync(Book);
    }
}