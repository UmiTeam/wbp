using System;
using Volo.Abp.DependencyInjection;

namespace Umi.Wbp.Message;

public class MessageService : IMessageService, ISingletonDependency
{
    public MessageQueue MessageQueue { get; } = new();

    public void ShowMessage(string message, TimeSpan? duration = null){
        MessageQueue.Enqueue(message, duration);
    }
}