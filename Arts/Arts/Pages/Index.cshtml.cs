using Arts.Pages.Users;
using Arts.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Arts.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string? ReturnUsername { get; set; }
        public int ? IsAdmin { get; set; }
        public int ReturnId { get; set; }
        public int Count { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? searchingWord { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        { 
            CountNbreItem nbreItem = new CountNbreItem();

            ReturnUsername = HttpContext.Session.GetString("username");

            try
            {
                string ConnectionString = "Data Source=.;Initial Catalog=Arts;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string sql = "SELECT IsAdmin FROM Users WHERE Username = '" + ReturnUsername + "';";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsAdmin = reader.GetInt32(0);
                                Console.WriteLine(IsAdmin);
                                return;
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //searchingWord = Request.Form["search"];
            //if (searchingWord == null)
            //{
            //    Console.WriteLine("C' est vide");
            //}
            //else
            //{
            //    Console.WriteLine(searchingWord);
            //}

        }

        public void OnPost()
        {
            /*ReturnUsername = "";
            HttpContext.Session.Remove("username");
            Response.Redirect("/Index");*/

            


            Console.WriteLine(searchingWord);
        }
    }
}