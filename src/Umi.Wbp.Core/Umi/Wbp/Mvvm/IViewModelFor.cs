using System.Windows;
using JetBrains.Annotations;

namespace Umi.Wbp.Mvvm;

public interface IViewModelFor<T> where T : FrameworkElement
{
    [CanBeNull] public T View { get; protected internal set; }
}