
- 选项接口
- IOptionsSnapshot与IOptionsMonitor的区别
- 选项的后期配置
- 选项的验证

在*ASP.NET Core笔记(3) - 配置*中介绍了各种配置提供程序以及配置的读取方式，但实际使用中，不推荐应用程序直接从一堆配置中读取的做法，而是使用强类型绑定，将配置按组绑定到不同服务所属的类。使用这种方式可以让配置方案遵守两个重要的软件工程原则：
- 接口分离原则 (ISP) 或封装 – 依赖于配置设置的方案（类）仅依赖于其使用的配置设置。
- 关注点分离 – 应用的不同部件的设置不彼此依赖或相互耦合。

### 选项接口
ASP.NET Core选项模式常用的接口有
- IOptions
- IOptionsSnapshot
- IOptionsMonitor

IOptions在配置更改时无法对应变更选项的值，只能重启应用。IOptionsSnapshot和IOptionsMonitor则具有这种能力。

选项可以作为服务在使用时被注入，下面的代码模拟了选项的使用场景,OrderServiceOptions作为OrderService的选项被注入，OrderService又在控制器中被注入：
```
public interface IOrderService
{
    int ShowMaxOrderCount();
}

public class OrderService : IOrderService
{
    IOptionsSnapshot<OrderServiceOptions> _options;
    public OrderService(IOptionsSnapshot<OrderServiceOptions> options)
    {
        _options = options;
    }

    public int ShowMaxOrderCount()
    {
        return _options.Value.MaxOrderCount;
    }
}

public class OrderServiceOptions
{
    public int MaxOrderCount { get; set; };
}
```
控制器中注入OrderService：
```
[HttpGet]
public string Get([FromServices]IOrderService orderService)
{
    var res = $"orderService.ShowMaxOrderCount:{orderService.ShowMaxOrderCount()},time={orderService.ShowTime()}";
    Console.WriteLine(res);
    return res;
}
```
在ConfigService配置OrderServiceOptions和OrderService的注入：
```
services.Configure<OrderServiceOptions>(Configuration.GetSection("OrderService"));
services.AddScoped<IOrderService, OrderService>();
```

### IOptionsSnapshot与IOptionsMonitor的区别
这里注入时使用的是AddScoped作用域的方式，是因为使用了IOptionsSnapshot接口。IOptionsSnapshot的生命周期为作用域，会在每次请求时应重新读取配置、更新选项。所以修改配置后，重新请求API，就可以看到最新的配置值。
如果尝试选择AddSingleton，由于OrderService单例的生命周期比IOptionsSnapshot更长，会直接抛出运行时异常。

而如果遇到既需要单例生命周期，还需要变更检测的场景时怎么办呢?这时就需要用到IOptionsMonitor了。
IOptionsMonitor 和 IOptionsSnapshot 之间的区别在于：
- IOptionsMonitor 是一种单例服务，且可以监测配置的变更。
- IOptionsSnapshot 是一种作用域服务，并在构造 IOptionsSnapshot<T> 对象时提供选项的快照。

IOptionsMonitor使用与IOptionsSnapshot类似，但取值变为CurrentValue。如果修改配置源，就会触发OnChange方法：
```
_options.OnChange(option =>
{
    Console.WriteLine($"配置更新了，最新的值是:{_options.CurrentValue.MaxOrderCount}");
});
```

IOptionsMonitor还可以结合*ASP.NET Core笔记(3) - 配置*中介绍的自定义数据源的方法，当IConfigurationProvider触发OnReload()事件时，这里的OnChange也就会被触发。

### 选项的后期配置
使用PostConfigure可进行选项的后期配置：
```
services.PostConfigure<OrderServiceOptions>(options =>
{
    options.MaxOrderCount += 20;
});
```

### 选项的验证
为了防止应用程序读取到错误的配置，可以为选项添加验证。
选项验证有三种方式：
- 直接注册验证函数
- DataAnnotations
- 实现IValidateOptions接口

#### 直接注册验证函数
添加选项需要替换为AddOptions
```
//services.Configure<OrderServiceOptions>(configuration);
services.AddOptions<OrderServiceOptions>().Bind(configuration).Configure(options =>
{
configuration.Bind(options);
})
.Validate(options => options.MaxOrderCount <= 100, "MaxOrderCount不能大于100");
```
#### DataAnnotations
调用ValidateDataAnnotations：
```
services.AddOptions<OrderServiceOptions>().Bind(configuration).Configure(options =>
{
configuration.Bind(options);
})
.ValidateDataAnnotations<OrderServiceOptions>();
```
为选项模型类添加Annotation:
```
public class OrderServiceOptions
{
    [Range(0, 100)]
    public int MaxOrderCount { get; set; };
}
```
#### 实现IValidateOptions接口
注册验证服务：
```
services.AddOptions<OrderServiceOptions>().Bind(configuration).Configure(options =>
{
configuration.Bind(options);
})
.Services.AddSingleton<IValidateOptions<OrderServiceOptions>,OrderServiceValidateOptions> ();
```
实现IValidateOptions：
```
public class OrderServiceValidateOptions : IValidateOptions<OrderServiceOptions>
{
    public ValidateOptionsResult Validate(string name, OrderServiceOptions options)
    {
        if (options.MaxOrderCount > 100)
        {
            return ValidateOptionsResult.Fail("MaxOrderCount不能大于100");
        }
        else
        {
            return ValidateOptionsResult.Success;
        }
    }
}
```