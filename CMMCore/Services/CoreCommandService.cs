using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.ExceptionServices;

namespace CMMCore.Services;

public class CoreCommandService : ICoreCommandService
{
    private const string _commandHandlerSuffix = "Handler";
    private const string _commandHandlerMethodName = "HandlerInvoke";
    private IServiceProvider _serviceProvider;

    public CoreCommandService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Process(ICoreCommand command)
    {
        try
        {
            ProcessInternal(command);
        }
        catch (Exception ex)
        {
            ExceptionDispatchInfo.Capture(ex).Throw();
        }
    }

    public async Task ProcessAsync(ICoreCommand command)
    {
        try
        {
            await Task.Run(() => ProcessInternal(command));
        }
        catch (Exception ex)
        {
            ExceptionDispatchInfo.Capture(ex).Throw();
        }
    }

    private void ProcessInternal(ICoreCommand command)
    {
        var commandName = command.GetType().FullName;
        var assemblyName = command.GetType().Assembly.FullName;
        var commandHandlerType = Type.GetType(commandName + _commandHandlerSuffix + "," + assemblyName);
        commandHandlerType = commandHandlerType ?? throw new CommandHandlerNotFoundException(commandName);

        var commandHandler = _serviceProvider.GetRequiredService(commandHandlerType);
        var commandHandlerMethod = commandHandlerType.GetMethod(_commandHandlerMethodName);
        commandHandlerMethod = commandHandlerMethod ?? throw new MethodNotFoundOnCommandHandlerException(command.ToString());

        commandHandlerMethod.Invoke(commandHandler, new object[] { command });
    }
}
