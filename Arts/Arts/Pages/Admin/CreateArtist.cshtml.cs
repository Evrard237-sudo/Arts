using Arts.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Arts.Pages.Admin
{
    public class CreateArtistModel : PageModel
    {
        public string errorMessage = "";
        public string successMessage = "";
        public Models.ArtistInfo artistInfo = new Models.ArtistInfo();
        public void OnGet()
        {
        }

        public void OnPost()
        {
            artistInfo.ArtistName = Request.Form["ArtistName"];
            artistInfo.ArtistEmail = Request.Form["ArtistEmail"];
            artistInfo.ArtistUrl = Request.Form["ArtistUrl"];
            artistInfo.ArtistThumbnail = Request.Form["ArtistThumbnail"];
            string sourcePath = @"D:\TRAVAUX ADO .NET\Images Projet";
            string targetPath = @"D:\TRAVAUX ADO .NET\Projet ADO\Arts\Arts\Arts\wwwroot\UploadImage\";
            CopyThumbail copyThumbail = new CopyThumbail();
            copyThumbail.CopyThumbnail(artistInfo.ArtistThumbnail, sourcePath, targetPath);

            // Sauvegarder les donnes dans la base de données 
            try
            {
                string connectionString = "Data Source=.;Initial Catalog=Arts;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Artists " +
                        "(ArtistName, ArtistEmail, ArtistUrl, ArtistThumbnail) VALUES " +
                        "(@ArtistName, @ArtistEmail, @ArtistUrl, @ArtistThumbnail);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ArtistName", artistInfo.ArtistName);
                        command.Parameters.AddWithValue("@ArtistEmail", artistInfo.ArtistEmail);
                        command.Parameters.AddWithValue("@ArtistUrl", artistInfo.ArtistUrl);
                        command.Parameters.AddWithValue("@ArtistThumbnail", "/UploadImage/" + artistInfo.ArtistThumbnail);
                        

                        command.ExecuteNonQuery();
                    }
                }

                Response.Redirect("/Admin/ArtistAdmin");
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                Response.Redirect("/Error404");
            }
        }
    }
}
