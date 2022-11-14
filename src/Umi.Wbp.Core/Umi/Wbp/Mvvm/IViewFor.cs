using JetBrains.Annotations;

namespace Umi.Wbp.Mvvm;

public interface IViewFor<T> where T : class
{
    [CanBeNull] public T ViewModel { get; protected internal set; }
}