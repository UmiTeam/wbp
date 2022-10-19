using Microsoft.Extensions.DependencyInjection;

namespace Umi.Wbp.Core;

public class IocHelper
{
    public static IServiceCollection Services { get; internal set; }
}