using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Umi.Wbp.Message;

public class MessageQueue
{
    private readonly TimeSpan defaultMessageDuration = TimeSpan.FromSeconds(2);
    private readonly EventWaitHandle eventWaitHandle = new ManualResetEvent(false);
    private readonly ConcurrentQueue<MessageQueueItem> messages = new();

    public EventWaitHandle EventWaitHandle => eventWaitHandle;

    public void Enqueue(string message, TimeSpan? duration){
        var messageQueueItem = new MessageQueueItem(message, duration ?? defaultMessageDuration);
        messages.Enqueue(messageQueueItem);
        eventWaitHandle.Set();
    }

    public bool TryDequeue(out MessageQueueItem message){
        message = null;
        if (messages.TryDequeue(out var item)){
            message = item;
            return true;
        }

        return false;
    }
}