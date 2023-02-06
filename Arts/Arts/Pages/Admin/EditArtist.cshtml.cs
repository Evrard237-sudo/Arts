using Arts.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Arts.Pages.Admin
{
    public class EditArtistModel : PageModel
    {
        public string successMessage = "";
        public string errorMessage = "";
        public Models.ArtistInfo artist = new Models.ArtistInfo(); 
        public void OnGet()
        {
            int Id = int.Parse(Request.Query["Id"]);

            try
            {
                string connectionString = "Data Source=.;Initial Catalog=Arts;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // Cette requete affiche l' item dont on a besoin 
                    string sql = "SELECT * FROM Artists WHERE Id = @Id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // La valeur de l' id
                        command.Parameters.AddWithValue("@Id", Id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // La lecture de ces differents elements
                                artist.Id = reader.GetInt32(0);
                                artist.ArtistName = reader.GetString(1);
                                artist.ArtistEmail = reader.GetString(2);
                                artist.ArtistUrl = reader.GetString(3);
                                artist.ArtistThumbnail = reader.GetString(4);

                                Console.WriteLine(artist.ArtistThumbnail);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // En cas d' erreurs retourne une exeption
                errorMessage = ex.Message;
                Console.WriteLine(errorMessage);
                Response.Redirect("/Error404");
            }
        }

        public void OnPost()
        {
            artist.Id = int.Parse(Request.Form["Id"]);
            artist.ArtistName = Request.Form["ArtistName"];
            artist.ArtistEmail = Request.Form["ArtistEmail"];
            artist.ArtistUrl = Request.Form["ArtistUrl"];
            artist.ArtistThumbnail = Request.Form["ArtistThumbnail"];

            string sourcePath = @"D:\TRAVAUX ADO .NET\Images Projet";
            string targetPath = @"D:\TRAVAUX ADO .NET\Projet ADO\Arts\Arts\Arts\wwwroot\UploadImage\";
            CopyThumbail copyThumbail = new CopyThumbail();
            copyThumbail.CopyThumbnail(artist.ArtistThumbnail, sourcePath, targetPath);

            try
            {
                string connectionString = "Data Source=.;Initial Catalog=Arts;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE Items " +
                                 "SET  ArtistName= @ArtistName, ArtistEmail= @ArtistEmail, ArtistUrl= @ArtistUrl, ArtistThumbnail= @ArtistThumbnail " +
                                 "WHERE Id= @Id;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", artist.Id);
                        command.Parameters.AddWithValue("@ArtistName", artist.ArtistName);
                        command.Parameters.AddWithValue("@ArtistEmail", artist.ArtistEmail);
                        command.Parameters.AddWithValue("@ArtistUrl", artist.ArtistUrl);
                        command.Parameters.AddWithValue("@ArtistThumbnail", "/UploadImage/" + artist.ArtistThumbnail);

                        command.ExecuteNonQuery();
                    }
                }

                Response.Redirect("/Admin/ItemAdmin");
            }
            catch (Exception ex)
            {
                errorMessage=ex.Message;
                Console.WriteLine(errorMessage);
                Response.Redirect("/Error404");
            }
        }
    }
}
