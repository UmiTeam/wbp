namespace Umi.Wbp.Routers;

public interface INavigationAware
{
    void OnNavigatedTo(NavigationContext navigationContext);
    void OnRefresh(NavigationContext navigationContext);
}