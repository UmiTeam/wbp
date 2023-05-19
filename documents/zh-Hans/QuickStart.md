# 快速开始

这是一个只包含单个项目的快速入门教程, 旨在教学如何在WPF项目中使用Wbp框架.

[//]: # (Wbp是一个开源应用程序框架,专注于基于WPF的Windows桌面应用程序开发.)

## 入门

在使用Wbp开发WPF应用之前,你需要有以下基础:

* 具备基础的C#和WPF知识.
* 了解<a href="https://docs.abp.io/zh-Hans/abp/6.0/Module-Development-Basics" target="_blank">ABP</a>框架的模块设计和依赖注入.

## 先决条件

* 一个集成开发环境 (比如: JetBrains Rider) 它需要支持 .NET 6.0+ 的WPF开发.

## 创建新的解决方案

我们将使用.NET CLI创建WPF的初始化项目. 在命令行终端中运行以下命令来创建一个WPF初始化项目:
<br/>`dotnet new wpf -f net6.0 -n Wbp.Tutorial.Wpf`<br/>
然后在集成开发环境中打开该项目.

## 安装Wbp框架

在项目文件夹中运行以下命令安装Wbp框架:
<br/>`Install-Package Umi.Wbp`<br/>

## 在WPF项目中配置Wbp框架

### 添加模块类

在项目根目录创建模块类, 模块类内容如下:

``` C#
[DependsOn(typeof(WbpModule))]
public class TutorialModule : AbpModule
{

}
```

注意事项: 模块类需要继承自`AbpModule`, 且依赖于`WbpModule`.

### 配置Router模块

注意: Router模块的详细配置教程会在该模块的教程中详细介绍, 此处仅作项目运行必须的配置.<br/>

在主界面(一般是`MainWindows.xaml`)中添加Wbp Router的渲染入口, 后续所有路由匹配到的组件将渲染在这里, 示例代码如下:

``` C#
<Window x:Class="Wbp.Tutorial.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:Wbp.Tutorial"
        xmlns:router="clr-namespace:Umi.Wbp.Routers;assembly=Umi.Wbp.Routers"
        mc:Ignorable="d"
        Title="MainWindow" Height="450" Width="800">
    <Grid>
        <router:RouterView RouterName="DefaultRouter"></router:RouterView>
    </Grid>
</Window>
```

注意事项:

* 需要添加Wbp Router的xaml引用, 即`xmlns:router="clr-namespace:Umi.Wbp.Routers;assembly=Umi.Wbp.Routers"`.
* 一个组件内存在多个RouterView时需要为添加Router View配置RouterName, 用于区分Router View .

在主界面后台代码(一般是`MainWindow.xaml.cs`)中添加Wbp Router的实现, 示例代码如下:

``` C#
public partial class MainWindow : Window, IRouterHost
{
    public MainWindow(){
        InitializeComponent();
    }
}
```

注意事项:

* 需要继承`IRouterHost`接口用于告知Wbp Router渲染入口, 即我们在主界面(一般是`MainWindow.xaml`)中添加的`RouterView`.

至此WPB Router的必须配置已配置完成, 更详细的使用教程请见Wbp Router模块.

### 配置`Application`

修改WPF应用即`App.xaml`如下:

``` C#
<wbp:WbpApplication x:TypeArguments="local:TutorialModule,local:MainWindow" x:Class="Umi.Wbp.Demo.App"
                    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                    xmlns:wbp="clr-namespace:Umi.Wbp;assembly=Umi.Wbp"
                    xmlns:local="clr-namespace:Umi.Wbp.Demo">
    <Application.Resources>

    </Application.Resources>
</wbp:WbpApplication>
```

注意事项:

* 添加Wbp Application的引用, 即`xmlns:wbp="clr-namespace:Umi.Wbp;assembly=Umi.Wbp"`.
* 修改`Application`为`wbp:WbpApplication`.
* 为`wbp:WbpApplication`添加泛型参数, 即`x:TypeArguments="local:DemoModule,local:MainWindow"`, 第一个参数为模块类即`TutorialModule`, 第二个为应用的主界面即`MainWindow`.
* 删除原应用的`StartupUri`配置, 否则启动应用会出现两个MainWindow窗体.

删除WPF应用后台代码(即`App.xaml.cs`)对`Application`类的继承, 示例代码如下:

``` C#
public partial class App
{

}
```

至此WPF应用中WPB框架的配置已完成, 开始使用Wbp开发应用吧!