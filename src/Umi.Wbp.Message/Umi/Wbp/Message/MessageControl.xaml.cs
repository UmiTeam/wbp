using System;
using System.Windows;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace Umi.Wbp.Message;

public partial class MessageControl : Border
{
    private readonly Dispatcher dispatcher = Dispatcher.FromThread(Thread.CurrentThread);
    private readonly Thread showMessageThread;
    private MessageQueueItem currentMessageQueueItem;

    public MessageControl(){
        InitializeComponent();
    }

    public void ShowMessage(MessageQueueItem messageQueueItem){
        currentMessageQueueItem = messageQueueItem;
        dispatcher.Invoke(() =>
        {
            Message = messageQueueItem.Message;
            var activeStoryboard = FindResource("ActivateStoryboard") as Storyboard;
            activeStoryboard?.Begin();
        });
        Thread.Sleep(messageQueueItem.Duration);
        if (currentMessageQueueItem == messageQueueItem){
            dispatcher.Invoke(() =>
            {
                var deactiveStoryboard = FindResource("DeactivateStoryboard") as Storyboard;
                deactiveStoryboard?.Begin();
            });
        }
    }

    #region Dependency

    public static readonly DependencyProperty MessageQueueProperty = DependencyProperty.Register(
        nameof(MessageQueue), typeof(MessageQueue), typeof(MessageControl),
        new PropertyMetadata(default(MessageQueue), MessageQueuePropertyChangedCallback));

    public MessageQueue MessageQueue
    {
        get => (MessageQueue)GetValue(MessageQueueProperty);
        set => SetValue(MessageQueueProperty, value);
    }

    private static void MessageQueuePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e){
        var wbpMessage = (MessageControl)dependencyObject;
        var messageQueue = e.NewValue as MessageQueue;
        messageQueue?.PairMessageControl(wbpMessage);
    }

    public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
        nameof(Message), typeof(string), typeof(MessageControl),
        new PropertyMetadata(default(string), propertyChangedCallback: MessagePropertyChangedCallback));

    public string Message
    {
        get => (string)GetValue(MessageProperty);
        set => SetValue(MessageProperty, value);
    }

    private static void MessagePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e){
        var wbpMessage = (MessageControl)dependencyObject;
        wbpMessage.TextBlock.Text = e.NewValue as string;
    }

    public static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register(
        nameof(Foreground), typeof(Brush), typeof(MessageControl), new PropertyMetadata(Brushes.White));

    public Brush Foreground
    {
        get => (Brush)GetValue(ForegroundProperty);
        set => SetValue(ForegroundProperty, value);
    }

    #endregion
}