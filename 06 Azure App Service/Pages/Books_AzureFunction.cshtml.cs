using AzureSQL_ServiceApp.Interface;
using AzureSQL_ServiceApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStore_AzureAppService.Pages
{
    public class Books_AzureFunctionModel : PageModel
    {

        public List<Books> Books;
        private readonly IBooksService _booksService;

        public Books_AzureFunctionModel(IBooksService booksService)
        {
            _booksService = booksService;
        }

        public void OnGet()
        {
            Books = _booksService.GetBooksByAzureFunction().GetAwaiter().GetResult();
        }
    }
}
