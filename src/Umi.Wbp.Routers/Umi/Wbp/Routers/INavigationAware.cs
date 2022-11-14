using Umi.Wbp.Core;

namespace Umi.Wbp.Routers;

public interface INavigationAware
{
    bool OnNavigatingTo(NavigationContext navigationContext);
    void OnNavigatedTo(NavigationContext navigationContext);
    bool OnNavigatingFrom(NavigationContext navigationContext);
    void OnNavigatedFrom(NavigationContext navigationContext);
}