using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Arts.Pages.Users
{
    public class IndexModel : PageModel
    {
        public List<UserInfo> ListUsers = new List<UserInfo>();
        public void OnGet()
        {
            try
            {
                string ConnectionString = "Data Source=.;Initial Catalog=Arts;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Users";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UserInfo userInfo = new UserInfo();
                                userInfo.Id = "" + reader.GetInt32(0);
                                userInfo.Username = reader.GetString(1);
                                userInfo.Useremail = reader.GetString(2);
                                userInfo.Userpassword = reader.GetString(3);
                                userInfo.IsAdmin = "" + reader.GetInt32(4);
                                userInfo.CreateAt = reader.GetDateTime(5).ToString();

                                ListUsers.Add(userInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class UserInfo
    {
        public string? Id;
        public string? Username;
        public string? Userpassword;
        public string? Useremail;
        public string? IsAdmin;
        public string? CreateAt;
    }
}
