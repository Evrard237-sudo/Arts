using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Arts.Pages.Users
{
    public class IndexModel : PageModel
    {
        public string? ReturnUsername { get; set; }
        public int? IsAdmin { get; set; }
        // La liste des utilisateurs
        public List<Models.UserInfo> ListUsers = new List<Models.UserInfo>();
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
                            }
                        }
                    }

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Response.Redirect("/Error404");
            }

            try
            {
                // La chaine de connexion a la base de donnée
                string ConnectionString = "Data Source=.;Initial Catalog=Arts;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                { // Connection a la BD
                    connection.Open();
                    // REquete sql permettant d' afficher la liste des utilisateurs
                    string sql = "SELECT * FROM Users";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {// La lecture des donnees 
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Models.UserInfo userInfo = new Models.UserInfo();
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
                // Si la lecture n' est pas possible, on retourne une exeption
                Console.WriteLine("Exception: " + ex.ToString());
                Response.Redirect("/Error404");
            }
        }
    }
    // La classe UserInfo est la classe qui dont les donnees vont servir pour etre connecter au donnees de la BD
}
