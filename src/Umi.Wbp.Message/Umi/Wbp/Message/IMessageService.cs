using System;

namespace Umi.Wbp.Message;

public interface IMessageService
{
    public MessageQueue MessageQueue { get; }
    void ShowMessage(string message, TimeSpan? duration = null);
}