using Arts.Pages.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Arts.Pages
{
    public class HomeModel : PageModel
    {
        public string? ReturnUsername { get; set; }
        public void OnGet()
        {
            ReturnUsername = HttpContext.Session.GetString("username");
        }
    }
}
