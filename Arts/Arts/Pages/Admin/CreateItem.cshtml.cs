using Arts.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Arts.Pages.Admin
{
    public class CreateItemModel : PageModel
    {
        public Models.ItemInfo itemInfo = new Models.ItemInfo(); 
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {

        }

        public void OnPost()
        {
            itemInfo.ItemName = Request.Form["ItemName"];
            itemInfo.ItemCategory = Request.Form["ItemCategorie"];
            itemInfo.ItemDescription = Request.Form["ItemDescription"];
            itemInfo.ItemThumbnail = Request.Form["ItemThumbnail"];
            itemInfo.ItemPrice = Request.Form["ItemPrice"];
            string sourcePath = @"D:\TRAVAUX ADO .NET\Images Projet";
            string targetPath = @"D:\TRAVAUX ADO .NET\Projet ADO\Arts\Arts\Arts\wwwroot\UploadImage\";
            CopyThumbail copyThumbail = new CopyThumbail();
            copyThumbail.CopyThumbnail(itemInfo.ItemThumbnail, sourcePath, targetPath);

            // Sauvegarder les donnes dans la base de données 
            try
            {
                string connectionString = "Data Source=.;Initial Catalog=Arts;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Items " +
                        "(ItemCategory, ItemName, ItemDescription, ItemThumbnail, ItemPrice) VALUES " +
                        "(@ItemCategory, @ItemName, @ItemDescription, @ItemThumbnail, @ItemPrice);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ItemCategory", itemInfo.ItemCategory);
                        command.Parameters.AddWithValue("@ItemName", itemInfo.ItemName);
                        command.Parameters.AddWithValue("@ItemDescription", itemInfo.ItemDescription);
                        command.Parameters.AddWithValue("@ItemThumbnail", "/UploadImage/" + itemInfo.ItemThumbnail);
                        command.Parameters.AddWithValue("@ItemPrice", itemInfo.ItemPrice);

                        command.ExecuteNonQuery();
                    }
                }

                Response.Redirect("/Admin/ItemAdmin");
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                Console.WriteLine(errorMessage);
                Response.Redirect("/Error404");
            }
        }
    }
}
