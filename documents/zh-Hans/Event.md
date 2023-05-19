# Wbp Event使用教程

Wbp事件模块使用的是<a href="https://github.com/PrismLibrary/Prism" target="_blank">Prism</a>框架的<a href="https://prismlibrary.com/docs/event-aggregator.html" target="_blank">Event Aggregator</a>, 使用教程请参考其官方文档.

## 注意事项

在Wbp Router中使用`Event Aggregator`时需要注意手动取消注册事件的监听, 否则会出现离开了某页面但因为GC并未及时进行回收, 页面依旧会响应事件的情况. 常见解决方案为在路由页面内的`INavigatedFromAware`钩子函数内取消事件的监听.