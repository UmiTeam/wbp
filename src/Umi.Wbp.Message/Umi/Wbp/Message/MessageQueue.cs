using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Umi.Wbp.Message;

public class MessageQueue
{
    private readonly TimeSpan defaultMessageDuration = TimeSpan.FromSeconds(2);
    private MessageControl messageControl;

    public void PairMessageControl(MessageControl messageControl){
        this.messageControl = messageControl;
    }

    public void Enqueue(string message, TimeSpan? duration){
        new TaskFactory().StartNew(() =>
        {
            messageControl.ShowMessage(new MessageQueueItem(message, duration ?? defaultMessageDuration));
        });
    }
}