using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Umi.Wbp.Test;

public class HelloWorldService : ITransientDependency
{
    public ILogger<HelloWorldService> Logger { get; set; }

    public HelloWorldService()
    {
        Logger = NullLogger<HelloWorldService>.Instance;
    }

    public string SayHello()
    {
        Logger.LogInformation("Call SayHello");
        return "Hello Wbp!";
    }

    public async Task<string> SayHelloAsync()
    {
        Task<string> task = new Task<string>(() =>
        {
            Logger.LogInformation("Call SayHello");
            Thread.Sleep(1000);
            return "Hello Wbp!";
        });
        task.Start();
        var result = await task.WaitAsync(TimeSpan.FromSeconds(3));
        return result;
    }
}