using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Arts.Pages.Bills
{
    public class BillFormModel : PageModel
    {
        public string? ReturnUsername { get; set; }
        public int? IsAdmin { get; set; }
        public void OnGet()
        {
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
        }
    }
}
