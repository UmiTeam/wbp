namespace Umi.Wbp.Routers;

public abstract class NavigationAwareAbstract : INavigationAware
{
    public virtual bool IsNavigationTarget(NavigationContext navigationContext) => true;
    public virtual bool CanNavigateFrom(NavigationContext navigationContext) => true;

    public virtual void OnNavigatedTo(NavigationContext navigationContext){
    }


    public virtual void OnNavigatedFrom(NavigationContext navigationContext){
    }
}