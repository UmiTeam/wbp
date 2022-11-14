using System.Windows;
using System.Windows.Controls;

namespace Umi.Wbp.Routers;

public class RouterView : ContentControl
{
    internal const string DefaultRouterViewName = nameof(DefaultRouterViewName);

    public static readonly DependencyProperty RouterNameProperty = DependencyProperty.Register(
        nameof(RouterName), typeof(string), typeof(RouterView), new PropertyMetadata(DefaultRouterViewName));

    public string RouterName
    {
        get => (string)GetValue(RouterNameProperty);
        set => SetValue(RouterNameProperty, value);
    }
}