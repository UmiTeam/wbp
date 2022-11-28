using System;

namespace Umi.Wbp.Routers;

[Obsolete($"Use {nameof(NavigationAwareAbstract)} class instead")]
public abstract class RoutableViewModel : INavigationAware
{
    public virtual bool OnNavigatingTo(NavigationContext navigationContext) => true;

    public virtual void OnNavigatedTo(NavigationContext navigationContext){
    }

    public virtual bool OnNavigatingFrom(NavigationContext navigationContext) => true;

    public virtual void OnNavigatedFrom(NavigationContext navigationContext){
    }
}