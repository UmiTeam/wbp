using System.Windows;

namespace Umi.Wbp.Mvvm;

public interface IViewModelFor<T> where T : FrameworkElement
{
    protected internal T View { get; set; }
}