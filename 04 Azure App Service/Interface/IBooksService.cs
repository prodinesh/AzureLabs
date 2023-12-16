using AzureSQL_ServiceApp.Model;

namespace AzureSQL_ServiceApp.Interface
{
    public interface IBooksService
    {
        List<Books> GetBooks();
        Task<bool> IsBeta();
    }
}
