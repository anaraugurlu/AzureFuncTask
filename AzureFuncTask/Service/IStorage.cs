namespace AzureFuncTask.Service
{
    public interface IStorage
    {
        Task Upload(IFormFile file);
    }
}
