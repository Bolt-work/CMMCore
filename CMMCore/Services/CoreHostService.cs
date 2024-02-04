using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Gotchi.Core.Services;

public abstract class CoreHostService
{
    protected IHost _host;
    protected IServiceScope _serviceScope;
    protected IServiceProvider _serviceProvider;

    public CoreHostService(string[] args)
    {
        _host = CreateHostBuilder(args).Build();
        _serviceScope = _host.Services.CreateScope();
        _serviceProvider = _serviceScope.ServiceProvider;
    }

    protected virtual IServiceCollection Registers(IServiceCollection service)
    {
        throw new NoDependencyInjectionRegisteredException();
    }

    protected IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, service) =>
            {
                Registers(service);
            });
    }
}
