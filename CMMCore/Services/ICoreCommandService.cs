namespace CMMCore.Services
{
    public interface ICoreCommandService
    {
        void Process(ICoreCommand command);
        Task ProcessAsync(ICoreCommand command);
    }
}