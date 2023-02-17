namespace Umi.Wbp.Mvvm;

public interface IViewFor<T> where T : class
{
    public T? ViewModel { get; protected internal set; }
}