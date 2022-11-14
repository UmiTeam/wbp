using System.Collections.Generic;
using Volo.Abp.DependencyInjection;

namespace Umi.Wbp.Routers;

public interface IRouterHost : ISingletonDependency
{
    public ICollection<RouterView> RouterViews { get; }
}