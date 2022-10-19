using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Umi.Wbp.Commands;
using Umi.Wbp.Events;
using Umi.Wbp.Mvvm;
using Umi.Wbp.Regions;
using Umi.Wbp.Test.Events;
using Volo.Abp.DependencyInjection;

namespace Umi.Wbp.Test.ViewModels;

public class TestViewModel : ObservableObject, ITransientDependency, INavigationAware
{
    private readonly HelloWorldService helloWorldService;
    private readonly IEventAggregator eventAggregator;
    private readonly IRegionService regionService;

    public TestViewModel(HelloWorldService helloWorldService, IEventAggregator eventAggregator, IRegionService regionService)
    {
        this.helloWorldService = helloWorldService;
        this.eventAggregator = eventAggregator;
        this.regionService = regionService;
        HelloWorldString = helloWorldService.SayHello();
        TestCommand = new AsyncRelayCommand(Test);
        eventAggregator.GetEvent<TestEvent>().Subscribe(hello => { HelloWorldString = hello + "Test event triggered!"; });
    }

    private string helloWorldString;

    public string HelloWorldString
    {
        get => helloWorldString;
        set => SetProperty(ref helloWorldString, value);
    }

    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        if (navigationContext.Parameters.TryGetValue("Identifier", out string identifier))
        {
            HelloWorldString = helloWorldService.SayHello() + identifier;
        }
        MessageBox.Show("To: " + HelloWorldString);
    }

    public bool IsNavigationTarget(NavigationContext navigationContext) => true;

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
        MessageBox.Show("From: " + HelloWorldString);
    }

    public ICommand TestCommand { get; set; }

    private async Task Test()
    {
        eventAggregator.GetEvent<TestEvent>().Publish(await helloWorldService.SayHelloAsync() + "Async invoked!!!");
    }
}