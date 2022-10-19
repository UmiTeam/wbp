using System;
using Umi.Wbp.Dialogs;
using Umi.Wbp.Events;
using Umi.Wbp.Mvvm;
using Umi.Wbp.Test.Events;
using Volo.Abp.DependencyInjection;

namespace Umi.Wbp.Test.ViewModels;

public class TestDialogViewModel : ObservableObject, IDialogAware, ITransientDependency
{
    private readonly HelloWorldService helloWorldService;
    private readonly IEventAggregator eventAggregator;

    public TestDialogViewModel(HelloWorldService helloWorldService, IEventAggregator eventAggregator)
    {
        this.helloWorldService = helloWorldService;
        this.eventAggregator = eventAggregator;
        HelloWorldString = helloWorldService.SayHello();
        eventAggregator.GetEvent<TestEvent>().Subscribe((hello) => { HelloWorldString = "Test event triggered!"; });
    }

    private string helloWorldString;

    public string HelloWorldString
    {
        get => helloWorldString;
        set => SetProperty(ref helloWorldString, value);
    }

    public bool CanCloseDialog()
    {
        return true;
    }

    public void OnDialogClosed()
    {
    }

    public void OnDialogOpened(IDialogParameters parameters)
    {
    }

    public string Title => helloWorldService.SayHello();
    public event Action<IDialogResult> RequestClose;
}