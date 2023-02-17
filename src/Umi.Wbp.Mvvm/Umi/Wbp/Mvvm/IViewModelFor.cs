using System.Windows;

namespace Umi.Wbp.Mvvm;

public interface IViewModelFor<T> where T : FrameworkElement
{
    public T? View { get; protected internal set; }
}