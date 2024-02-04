using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gotchi.Core.Services;

public class ServiceExceptionBase : Exception
{
    public ServiceExceptionBase(string message) : base(message) { }
}

public class CommandHandlerNotFoundException : ServiceExceptionBase 
{
    private const string _message = "No command handler found for command :: ";
    public CommandHandlerNotFoundException(string? commandName) : base(_message + commandName) { }
}

public class MethodNotFoundOnCommandHandlerException : ServiceExceptionBase
{
    private const string _message = "Method not found on commandHandler :: ";
    public MethodNotFoundOnCommandHandlerException(string? handlerName) : base(_message + handlerName) { }
}

public class NoDependencyInjectionRegisteredException : ServiceExceptionBase
{
    private const string _message = "No dependency injection  has been registered";
    public NoDependencyInjectionRegisteredException() : base(_message) { }
}
