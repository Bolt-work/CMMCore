using Microsoft.Extensions.DependencyInjection;
using System.Runtime.ExceptionServices;

namespace Gotchi.Core.Services;

public class CoreCommandService : ICoreCommandService
{
    private const string _commandHandlerSuffix = "Handler";
    private const string _commandHandlerMethodName = "HandlerInvoke";
    private IServiceProvider _serviceProvider;

    public CoreCommandService(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    public void ProcessX(ICoreCommand command)
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
        var commandHandlerType = Type.GetType(commandName + _commandHandlerSuffix);
        commandHandlerType = commandHandlerType ?? throw new CommandHandlerNotFoundException(commandName);

        var commandHandler = _serviceProvider.GetRequiredService(commandHandlerType);
        var commandHandlerMethod = commandHandlerType.GetMethod(_commandHandlerMethodName);
        commandHandlerMethod = commandHandlerMethod ?? throw new MethodNotFoundOnCommandHandlerException(command.ToString());

        commandHandlerMethod.Invoke(commandHandler, new object[] { command });
    }
}
