using Arts.Pages.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Arts.Pages.Gallery
{
    public class ListGalleryModel : PageModel
    {
        public string? ReturnUsername { get; set; }
        public int? IsAdmin { get; set; }
        public List<Models.galleryInfo> ListGallery = new List<Models.galleryInfo>();
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
                                Models.galleryInfo info = new Models.galleryInfo();
                                info.Id = reader.GetInt32(0);
                                info.Thumbnail = reader.GetString(1);
                                info.CreateAt = reader.GetDateTime(2).ToString();

                                ListGallery.Add(info);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exeption : " + ex.Message);
                Response.Redirect("/Error404");
            }
        }
    }
}
