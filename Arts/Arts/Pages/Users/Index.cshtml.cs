using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Arts.Pages.Users
{
    public class IndexModel : PageModel
    {
        public List<UserInfo> userInfos = new List<UserInfo>(); 
        public void OnGet()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exeption" + ex.ToString());
            }
        }
    }

    public class UserInfo
    {
        public int Id;
        public string? Username;
        public string? Useremail;
        public string? Userpassword;
        public int IsAdmin;
        public string? CreateAt;
    }
}
