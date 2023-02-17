namespace Umi.Wbp.Routers;

public interface INavigatedFromAware
{
    /// <summary>
    /// 路由离开页面时触发.
    /// </summary>
    /// <param name="navigationContext"></param>
    void OnNavigatedFrom(NavigationContext navigationContext);
}