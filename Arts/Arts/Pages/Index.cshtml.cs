using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Arts.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string? ReturnUsername { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            ReturnUsername = HttpContext.Session.GetString("username");
        }

        public void OnPost()
        {
            ReturnUsername = "";
            HttpContext.Session.Remove("username");
            Response.Redirect("/Index");
        }
    }
}