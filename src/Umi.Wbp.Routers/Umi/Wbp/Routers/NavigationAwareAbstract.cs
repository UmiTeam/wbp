namespace Umi.Wbp.Routers;

public abstract class NavigationAwareAbstract : INavigationAware
{
    public virtual void OnNavigatedTo(NavigationContext navigationContext){
    }

    public virtual void OnRefresh(NavigationContext navigationContext){
    }
}