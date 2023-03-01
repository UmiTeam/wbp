using System.Windows;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace Umi.Wbp.Message;

public partial class MessageControl : Border
{
    private readonly Dispatcher dispatcher = Dispatcher.FromThread(Thread.CurrentThread);
    private readonly Thread showMessageThread;
    public MessageControl(){
        InitializeComponent();
        showMessageThread = new Thread(ShowMessage);
        showMessageThread.Start();
    }

    private void ShowMessage(){
        while (true){
            MessageQueue messageQueue = null;
            try{
                dispatcher.Invoke(() => { messageQueue = MessageQueue; });
            }
            catch (TaskCanceledException){
                return;
            }

            messageQueue?.EventWaitHandle.WaitOne();

            if (messageQueue.TryDequeue(out var item)){
                try{
                    dispatcher.Invoke(() =>
                    {
                        var storyboard = FindResource("ActivateStoryboard") as Storyboard;
                        storyboard?.Begin();
                        Message = item.Message;
                    });
                }
                catch (TaskCanceledException){
                    return;
                }

                Thread.Sleep(item.Duration);
                try{
                    dispatcher.Invoke(() =>
                    {
                        var storyboard = FindResource("DeactivateStoryboard") as Storyboard;
                        storyboard?.Begin();
                    });
                }
                catch (TaskCanceledException){
                    return;
                }
            }
            else{
                messageQueue.EventWaitHandle.Set();
            }
        }
    }

    #region Dependency

    public static readonly DependencyProperty MessageQueueProperty = DependencyProperty.Register(
        nameof(MessageQueue), typeof(MessageQueue), typeof(MessageControl),
        new PropertyMetadata(default(MessageQueue)));

    public MessageQueue MessageQueue
    {
        get => (MessageQueue)GetValue(MessageQueueProperty);
        set => SetValue(MessageQueueProperty, value);
    }

    public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
        nameof(Message), typeof(string), typeof(MessageControl),
        new PropertyMetadata(default(string), propertyChangedCallback: PropertyChangedCallback));

    public string Message
    {
        get => (string)GetValue(MessageProperty);
        set => SetValue(MessageProperty, value);
    }

    private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e){
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