using Microsoft.Extensions.DependencyInjection;
using Program.SceneModels;
using Program.Services;

namespace Program;

public static class Program
{
    /// <summary>
    /// IOC 容器，用于提供服务
    /// </summary>
    public static readonly ServiceProvider Services = ConfigureServices();

    /// <summary>
    /// 配置服务并构建服务提供者
    /// </summary>
    /// <returns>返回构建完毕的服务提供者</returns>
    private static ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        AddServices(services);
        AddSceneModel(services);
        return services.BuildServiceProvider();
    }

    /// <summary>
    /// 向集合中添加服务，应以 Singleton 形式添加
    /// </summary>
    /// <param name="services">一个 IServiceCollection 实例，用于添加服务</param>
    private static void AddServices(IServiceCollection services)
    {
        services.AddSingleton<TestService>();
    }

    /// <summary>
    /// 向集合中添加场景模型，应以 Transient 添加
    /// </summary>
    /// <param name="serviceCollection">一个 IServiceCollection 实例，用于添加服务</param>
    private static void AddSceneModel(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<MainSceneModel>();
    }
}