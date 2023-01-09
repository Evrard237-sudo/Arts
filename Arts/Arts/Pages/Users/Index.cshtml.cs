using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Arts.Pages.Users
{
    public class IndexModel : PageModel
    {
        // La liste des utilisateurs
        public List<UserInfo> ListUsers = new List<UserInfo>();
        public void OnGet()
        {
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
                // Si la lecture n' est pas possible, on retourne une exeption
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }
    // La classe UserInfo est la classe qui dont les donnees vont servir pour etre connecter au donnees de la BD
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
