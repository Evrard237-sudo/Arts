using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Arts.Pages.Admin
{
    public class ArtistAdminModel : PageModel
    {
        public string? ReturnUsername { get; set; }
        public int? IsAdmin { get; set; }
        public List<Models.ArtistInfo> ListArtist = new List<Models.ArtistInfo>();
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
                string connectionString = "Data Source=.;Initial Catalog=Arts;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Connexion a la BD
                    connection.Open();
                    //Requete sql permettant d' afficher la liste des artistes inserer dans la base de données
                    string sql = "SELECT * FROM Artists";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // La lecture des données de la table Artists
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Models.ArtistInfo artist = new Models.ArtistInfo();
                                artist.Id = reader.GetInt32(0);
                                artist.ArtistName = reader.GetString(1);
                                artist.ArtistEmail = reader.GetString(2);
                                artist.ArtistUrl = reader.GetString(3);
                                artist.ArtistThumbnail = reader.GetString(4);

                                ListArtist.Add(artist);
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
