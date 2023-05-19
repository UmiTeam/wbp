# WBP Router使用教程

当使用Wbp Router时，我们需要做的就是将我们的组件映射到路由上，让Wbp Router知道在哪里渲染它们。下面是一个简单的例子:

<br/>`MainWindow.xaml`<br/>

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
    <StackPanel>
        <router:RouterView RouterName="HeaderRouter"></router:RouterView>
        <router:RouterView RouterName="ContentRouter"></router:RouterView>
        <router:RouterView RouterName="FooterRouter"></router:RouterView>
    </StackPanel>
</Window>
```

<br/>`MainWindow.xaml.cs`<br/>

``` C#
public partial class MainWindow : Window, IRouterHost
{
    public MainWindow(){
        InitializeComponent();
    }
}
```

请注意`RouterView`将显示与url对应的组件。你可以把它放在任何地方，以适应你的布局。

## Wbp Router Route路径配置

Wbp Router通过url将组件与`RouterView`对应, 并将组建渲染在`RouterView`内, Route的配置示例如下:

``` C#
[DependsOn(typeof(WbpModule))]
public class TutorialModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context){
        Configure<WbpRouterOptions>(options =>
        {
            options.Routes = new List<Route>()
            {
                new()
                {
                    Path = "/",
                    Components = new()
                    {
                        { "HeaderRouter", typeof(HeaderView) },
                        { "ContentRouter", typeof(ContentView) },
                        { "FooterRouter", typeof(FooterView) }
                    }
                },
                new()
                {
                    Path = "/home",
                    Components = new()
                    {
                        { "ContentRouter", typeof(HomeView) }
                    }
                }
            };
        });
    }
}
```

## 嵌套路由

一些应用程序的 UI 由多层嵌套的组件组成。在这种情况下，URL 的片段通常对应于特定的嵌套组件结构，例如：

```
/user/profile                     /user/posts
+------------------+                  +-----------------+
| User             |                  | User            |
| +--------------+ |                  | +-------------+ |
| | Profile      | |  +------------>  | | Posts       | |
| |              | |                  | |             | |
| +--------------+ |                  | +-------------+ |
+------------------+                  +-----------------+
```

通过Wbp Router, 你可以使用嵌套路由配置来表达这种关系。基于上述的示例, 假设我们有一个UserView, 页面内容如下:

``` C#
<UserControl x:Class="Wbp.Tutorial.Wpf.Views.User.UserView"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
             xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
             xmlns:local="clr-namespace:Wbp.Tutorial.Wpf.Views.User"
             xmlns:router="clr-namespace:Umi.Wbp.Routers;assembly=Umi.Wbp.Routers"
             mc:Ignorable="d"
             d:DesignHeight="300" d:DesignWidth="300">
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="1*"></RowDefinition>
            <RowDefinition Height="10*"></RowDefinition>
        </Grid.RowDefinitions>
        <StackPanel>
            <TextBlock>This is user view header</TextBlock>
            <Button Click="GoProfile">Go Profile View</Button>
            <Button Click="GoPost">Go Post View</Button>
        </StackPanel>
        <router:RouterView Grid.Row="1"></router:RouterView>
    </Grid>
</UserControl>
```

一个被渲染的组件内可以包含自己嵌套的 `RouterView`, 这里的 `RouterView` 就是一个内嵌的 `RouterView`。

要将组件渲染到这个嵌套的 `RouterView` 中，我们需要在路由中配置 `Children`, Route配置如下:

``` C#
[DependsOn(typeof(WbpModule))]
public class TutorialModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context){
        Configure<WbpRouterOptions>(options =>
        {
            options.Routes = new List<Route>()
            {
                new()
                {
                    Path = "/",
                    Components = new()
                    {
                        { "HeaderRouter", typeof(HeaderView) },
                        { "ContentRouter", typeof(ContentView) },
                        { "FooterRouter", typeof(FooterView) }
                    }
                },
                new()
                {
                    Path = "/home",
                    Components = new()
                    {
                        { "ContentRouter", typeof(HomeView) }
                    }
                },
                new()
                {
                    Path = "/user",
                    Components = new()
                    {
                        { "ContentRouter", typeof(UserView) }
                    },
                    Children = new List<Route>()
                    {
                        new()
                        {
                            Path = "/profile",
                            Component = typeof(ProfileView)
                        },
                        new()
                        {
                            Path = "/post",
                            Component = typeof(PostView)
                        }
                    }
                },
            };
        });
    }
}
```

如你所见，`Children` 配置只是另一个路由数组，就像 `Route` 本身一样。因此，你可以根据自己的需要，不断地嵌套视图。

此时，按照上面的配置，当你访问 `/user` 时，在 `User` 的 `RouterView` 里面什么都不会呈现，因为没有匹配到嵌套路由。

## 导航

Wbp Router的导航通过`RouterService`实现, 通过在需要使用的类里面注入`IRouterService`来使用`RouterService`, 示例代码如下:

``` C#
public partial class UserView : UserControl, ITransientDependency
{
    private readonly IRouterService routerService;
    public UserView(IRouterService routerService){
        this.routerService = routerService;
        InitializeComponent();
    }

    private void GoProfile(object sender, RoutedEventArgs e){
        routerService.Push("/user/profile");
    }

    private void GoPost(object sender, RoutedEventArgs e){
        routerService.Push("/user/post");
    }
}
```

### 导航参数

使用Wbp Router导航时可以向导航的页面传递参数, 示例如下:

``` C#
routerService.Push("/user/post",new Parameters(){{"Para1","This is parameter 1"}});
```

导航的页面想要接收到Wbp Router传递的参数, 需要实现`INavigatedToAware`接口的`OnNavigatedTo`函数, 示例代码如下:

``` C#
public partial class ProfileView : UserControl, ITransientDependency, INavigatedToAware
{
    public ProfileView(){
        InitializeComponent();
    }

    public void OnNavigatedTo(NavigationContext navigationContext){
        if (navigationContext.Parameters.TryGetValue("Para1", out string para)){
            MessageBox.Show(para);
        }
    }
}
```

### 导航历史

使用Wbp Router导航时, 可以向前或向后导航, 示例代码如下:

``` C#
routerService.GoBack(); // 回到上一次的页面.
routerService.GoForward(); // 回到前序页面.
```

### 导航守卫

正如其名，Wbp Router 提供的导航守卫主要用来通过跳转或取消的方式守卫导航。通过全局的方式植入路由导航中.

#### 全局前置守卫

你可以使用 router.beforeEach 注册一个全局前置守卫, 示例代码如下:

``` C#
options.BeforeEach=(context, next) =>
{
    next(context.To, true);  // context.To
    next(context.To, false); // false取消导航
};
```

守卫方法有两个参数：
context: 路由上下文, 包括路由参数, to路由地址, from路由地址.
next: 路由回调, 接收两个参数; 第一个参数表示导航前往的地址, 第二个表示是否可以导航.

#### 全局后置钩子

你也可以注册全局后置钩子，在导航完成后进行后续操作:

``` C#
options.AfterEach = (context) =>
{
    MessageBox.Show($"Navigate to {context.To} success");
};
```

#### 组件内钩子函数

Wbp Router为每个组件也准备了相应的钩子函数:

* `INavigatedToAware`: 当导航至组件时触发.
* `INavigatedFromAware`: 当从组件离开时触发.
* `IRefreshAware`: 当组件已经渲染在RouterView中, 但新的导航依旧需要在改RouterView渲染该组件时触发, 虽然Wbp Router不会重新渲染该组件, 但会触发刷新的钩子函数, 由用户进行处理.

注意事项: Wbp Router进行导航时不仅会触发组件视图内的钩子函数, 也会触发组件视图`DataContext`绑定的ViewModel内的钩子函数.

## 注意事项

* 所有需要Wbp Router进行导航的页面需要手动注册到IOC容器中, Wbp使用了<a href="https://autofac.org/" target="_blank">AutoFac</a>依赖注入框架,兼容<a href="https://abp.io/" target="_blank">ABP</a>依赖注入相关接口.