using System.Windows.Input;
using Umi.Wbp.Commands;
using Umi.Wbp.Message;
using Umi.Wbp.Routers;
using Volo.Abp.DependencyInjection;

namespace WbpTutorial.Wpf.Views;

public class MainWindowViewModel : ISingletonDependency
{
    private readonly IRouterService routerService;
    private readonly IMessageService messageService;

    public MainWindowViewModel(IRouterService routerService, IMessageService messageService){
        this.routerService = routerService;
        this.messageService = messageService;
        ListBookCommand = new RelayCommand(ListBook);
    }

    public MessageQueue MessageQueue => messageService.MessageQueue;
    public ICommand ListBookCommand { get; set; }

    private void ListBook(){
        routerService.Push("/list-book");
    }
}