using System;
using System.Threading.Tasks;
using System.Windows.Input;
using PropertyChanged;
using Umi.Wbp.Commands;
using Umi.Wbp.Message;
using WbpTutorial.Books;
using WbpTutorial.Domain.Books;

namespace WbpTutorial.Wpf.Views.Books;

[AddINotifyPropertyChangedInterface]
public class CreateBookViewModel
{
    private readonly IBookAppService bookAppService;
    private readonly IMessageService messageService;

    public CreateBookViewModel(IBookAppService bookAppService, IMessageService messageService){
        this.bookAppService = bookAppService;
        this.messageService = messageService;
        CreateBookCommand = new AsyncRelayCommand(CreateBookAsync);
    }

    public Book Book { get; set; } = new();
    public ICommand CreateBookCommand { get; set; }

    private async Task CreateBookAsync(){
        await bookAppService.CreateAsync(Book);
        messageService.ShowMessage("Create book success!",TimeSpan.FromSeconds(5));
    }
}