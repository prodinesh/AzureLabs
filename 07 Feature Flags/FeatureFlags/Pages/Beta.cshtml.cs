using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.FeatureManagement.Mvc;

namespace FeatureFlags.Pages
{
    [FeatureGate("Beta")]
    public class BetaModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
