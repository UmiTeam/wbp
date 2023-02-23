using System.Windows.Input;
using Umi.Wbp.Commands;
using Umi.Wbp.Routers;
using Volo.Abp.DependencyInjection;

namespace WbpTutorial.Wpf.Views;

public class MainWindowViewModel : ISingletonDependency
{
    private readonly IRouterService routerService;

    public MainWindowViewModel(IRouterService routerService){
        this.routerService = routerService;

        ListBookCommand = new RelayCommand(ListBook);
    }

    public ICommand ListBookCommand { get; set; }

    private void ListBook(){
        routerService.Push("/list-book");
    }
}