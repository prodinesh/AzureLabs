using AzureSQL_ServiceApp.DAL;
using AzureSQL_ServiceApp.Interface;
using AzureSQL_ServiceApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.FeatureManagement;

namespace AzureSQL_ServiceApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IBooksService _IBooksService;
        public List<Books> Books;
        public bool IsBeta;

        public IndexModel(IBooksService IBooksService) 
        {
            _IBooksService = IBooksService;
        }

        public void OnGet()
        {
            Books = _IBooksService.GetBooks();
            IsBeta = _IBooksService.IsBeta().Result;
        }
    }
}
