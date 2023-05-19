namespace Umi.Wbp.Mvvm;

/// <summary>
/// 继承该接口可自动为View注入ViewModel
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IViewFor<T> where T : class
{
    /// <summary>
    /// View的ViewModel
    /// </summary>
    protected internal T ViewModel { get; set; }
}