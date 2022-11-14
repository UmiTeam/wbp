# WBP Framework

![build and test](https://img.shields.io/github/workflow/status/UmiTeam/wbp/publish%20to%20nuget?style=flat-square)
[![NuGet](https://img.shields.io/nuget/v/Umi.Wbp.Core.svg?style=flat-square)](https://www.nuget.org/packages/Umi.Wbp.Core)
[![NuGet Download](https://img.shields.io/nuget/dt/Umi.Wbp.Core.svg?style=flat-square)](https://www.nuget.org/packages/Umi.Wbp.Core)
[![Language](https://img.shields.io/github/languages/top/UmiTeam/wbp)](https://github.com/UmiTeam/wbp)
[![Issues](https://img.shields.io/github/issues/UmiTeam/wbp)](https://github.com/UmiTeam/wbp/issues)
[![PullRequest](https://img.shields.io/github/issues-pr/UmiTeam/wbp)](https://github.com/UmiTeam/wbp/pulls)
[![License](https://img.shields.io/github/license/UmiTeam/wbp)](https://github.com/UmiTeam/wbp/blob/master/LICENSE.md)


WBP Framework is based on the **ABP Framework** & **Prism** & **.NET Community Toolkit** to create **modern wpf applications**.

## Getting Started

### Quick Start

- Start new WPF project
- Install _Umi.Wbp_ nuget: ``` Install-Package Umi.Wbp ```
- Create application module:

``` C#
[DependsOn(typeof(WbpModule))]
public class DemoModule : AbpModule
{
    
}
```

- Edit MainWindow.xaml.cs to following:

``` C#
public partial class MainWindow : Window, IRouterHost
{
    public MainWindow(){
        InitializeComponent();
    }

    public ICollection<RouterView> RouterViews => new List<RouterView> { };
}
```

- Edit App.xaml to following:

``` C#
<wbp:WbpApplication x:TypeArguments="local:DemoModule,local:MainWindow" x:Class="Umi.Wbp.Demo.App"
                    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                    xmlns:wbp="clr-namespace:Umi.Wbp;assembly=Umi.Wbp"
                    xmlns:local="clr-namespace:Umi.Wbp.Demo">
    <Application.Resources>

    </Application.Resources>
</wbp:WbpApplication>
```

- Edit App.xaml.cs to following:

``` C#
public partial class App
{
}
```
- Enjoy Wbp!
### Startup Templates

TODO

## Related Links

* <a href="https://abp.io/" target="_blank">ABP Framework</a>
* <a href="https://github.com/PrismLibrary/Prism" target="_blank">Prism</a>
* <a href="https://github.com/CommunityToolkit/dotnet" target="_blank">.NET Community Toolkit</a>
