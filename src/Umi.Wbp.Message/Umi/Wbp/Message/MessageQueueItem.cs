using System;

namespace Umi.Wbp.Message;

public class MessageQueueItem
{
    public MessageQueueItem(string message, TimeSpan duration){
        Message = message;
        Duration = duration;
    }

    public string Message { get; set; }
    public TimeSpan Duration { get; set; }
}