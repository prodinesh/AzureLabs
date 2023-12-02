using AzureSQL_ServiceApp.DAL;
using AzureSQL_ServiceApp.Interface;
using AzureSQL_ServiceApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AzureSQL_ServiceApp.Pages
{
    public class IndexModel : PageModel
    {
        //public List<Courses> Courses;
        private readonly IBooksService _IBooksService;
        public List<Books> Books;

        public IndexModel(IBooksService IBooksService) 
        {
            _IBooksService = IBooksService;
        }

        public void OnGet()
        {
            //SQLConnectionClass DBService = new SQLConnectionClass();
            //Courses = DBService.GetCourses();
            Books = _IBooksService.GetBooks();
        }
    }
}
