namespace CMMCore.Services
{
    public interface ICoreCommandService
    {
        Task ProcessAsync(ICoreCommand command);
    }
}