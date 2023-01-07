using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Arts.Pages.Users
{
    public class LogInModel : PageModel
    {
        public UserInfo userInfo = new UserInfo();
        public void OnGet()
        {
        }
    }
}
