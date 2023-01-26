using Arts.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Arts.Pages.Admin
{
    public class CreateGalleryModel : PageModel
    {
        public string errorMessage = "";
        public string successMessage = "";
        public galleryInfo galleryInfo = new galleryInfo();
        public void OnGet()
        {
        }

        public void OnPost()
        {
            galleryInfo.Thumbnail = Request.Form["Thumbnail"];

            if (galleryInfo.Thumbnail.Length == 0)
            {
                errorMessage = "Veuillez inserer une image !!!";
                return;
            }

            string sourcePath = @"D:\TRAVAUX ADO .NET\Images Projet";
            string targetPath = @"D:\TRAVAUX ADO .NET\Projet ADO\Arts\Arts\Arts\wwwroot\UploadImage\";
            CopyThumbail copyThumbail = new CopyThumbail();
            copyThumbail.CopyThumbnail(galleryInfo.Thumbnail, sourcePath, targetPath);

            // Sauvegarder les donnes dans la base de données 
            try
            {
                string connectionString = "Data Source=.;Initial Catalog=Arts;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Gallery " +
                        "(GalleryThumbnail) VALUES " +
                        "(@GalleryThumbnail);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@GalleryThumbnail", "/UploadImage/" + galleryInfo.Thumbnail);     

                        command.ExecuteNonQuery();
                    }
                }

                Response.Redirect("/Admin/GalleryAdmin");
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
    }
}
