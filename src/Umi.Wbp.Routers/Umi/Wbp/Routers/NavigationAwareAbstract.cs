namespace Umi.Wbp.Routers;

public abstract class NavigationAwareAbstract : INavigationAware
{
    public virtual bool OnNavigatingTo(NavigationContext navigationContext) => true;

    public virtual void OnNavigatedTo(NavigationContext navigationContext){
    }

    public virtual bool OnNavigatingFrom(NavigationContext navigationContext) => true;

    public virtual void OnNavigatedFrom(NavigationContext navigationContext){
    }
}