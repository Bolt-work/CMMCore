namespace Gotchi.Core.Services
{
    public interface ICoreCommandService
    {
        Task ProcessAsync(ICoreCommand command);
    }
}