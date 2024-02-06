using CMMCore.Common;
using Microsoft.Extensions.Logging;

namespace CMMCore.Services;

public abstract class CoreCommandHandlerBase<T>
{
    ILogger<CoreCommandHandlerBase<T>> _logger;
    public CoreCommandHandlerBase(ILogger<CoreCommandHandlerBase<T>> logger)
    {
        _logger = logger;
    }

    public void HandlerInvoke(T command) 
    {
        try
        {
            if(command is null)
                throw new ArgumentNullException(nameof(command));

            _logger.LogInformation($"Processing command {command.GetType().FullName}");
            Handle(command);
        }
        catch(CoreException ex)
        {
            _logger.LogError(ex.ToString());
        }
    }

    public abstract void Handle(T command);
}
