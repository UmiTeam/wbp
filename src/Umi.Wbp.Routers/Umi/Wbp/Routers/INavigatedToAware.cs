namespace Umi.Wbp.Routers;

public interface INavigatedToAware
{
    /// <summary>
    /// 路由到达页面时触发.
    /// </summary>
    /// <param name="navigationContext"></param>
    void OnNavigatedTo(NavigationContext navigationContext);
}