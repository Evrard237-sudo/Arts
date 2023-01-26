using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace Arts.Pages.Admin
{
    public class GalleryAdminModel : PageModel
    {
        public string? ReturnUsername { get; set; }
        public int? IsAdmin { get; set; }
        public List<galleryInfo> GalleryList = new List<galleryInfo>();
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
            }

            try
            {
                string ConnectionString = "Data Source=.;Initial Catalog=Arts;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                { // Connection a la BD
                    connection.Open();
                    // REquete sql permettant d' afficher la liste des utilisateurs
                    string sql = "SELECT * FROM Gallery";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {// La lecture des donnees 
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                galleryInfo info = new galleryInfo();
                                info.Id = reader.GetInt32(0);
                                info.Thumbnail = reader.GetString(1);
                                info.CreateAt = reader.GetDateTime(2).ToString();

                                GalleryList.Add(info);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exeption : " + ex.Message);
            }
        }
    }

    public class galleryInfo
    {
        public int Id;
        public string? Thumbnail;
        public string? CreateAt;
    }
}
