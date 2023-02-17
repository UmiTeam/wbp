namespace Umi.Wbp.Routers;

public interface IRefreshAware
{
    /// <summary>
    /// 路由刷新页面时触发.
    /// </summary>
    /// <param name="navigationContext"></param>
    void OnRefresh(NavigationContext navigationContext);
}