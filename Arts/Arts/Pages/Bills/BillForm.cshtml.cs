using Arts.Pages.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Primitives;
using System.Data.SqlClient;

namespace Arts.Pages.Bills
{
    public class BillFormModel : PageModel
    {
        public string? ReturnUsername { get; set; }
        public int? IsAdmin { get; set; }
        public UserInfo userInfo = new UserInfo();
        public void OnGet()
        {
            ReturnUsername = HttpContext.Session.GetString("username");

            try
            {
                string ConnectionString = "Data Source=.;Initial Catalog=Arts;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Users WHERE Username = '" + ReturnUsername + "';";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                userInfo.Id = "" + reader.GetInt32(0);
                                userInfo.Username = reader.GetString(1);
                                userInfo.Useremail = reader.GetString(2);
                                userInfo.Userpassword = reader.GetString(3);
                                userInfo.IsAdmin = "" + reader.GetInt32(4);
                                userInfo.CreateAt = reader.GetDateTime(5).ToString();
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

        public void OnPost()
        {

        }
    }
}
