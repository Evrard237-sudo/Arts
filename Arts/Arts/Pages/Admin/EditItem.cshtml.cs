using Arts.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Arts.Pages.Admin
{
    public class EditItemModel : PageModel
    {
        public ItemInfo itemInfo = new ItemInfo();
        public string errorMessage = "";
        public string successMessage = "";
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
                    string sql = "SELECT * FROM Items WHERE Id = @Id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // La valeur de l' id
                        command.Parameters.AddWithValue("@Id", Id);  
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // La lecture de ces differents elements
                                itemInfo.Id = reader.GetInt32(0);
                                itemInfo.ItemCategory = reader.GetString(1);
                                itemInfo.ItemName = reader.GetString(2);
                                itemInfo.ItemDescription = reader.GetString(3);
                                itemInfo.ItemThumbnail = reader.GetString(4);
                                itemInfo.ItemPrice = reader.GetDouble(5).ToString();

                                Console.WriteLine(itemInfo.ItemThumbnail);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // En cas d' erreurs retourne une exeption
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            itemInfo.Id = int.Parse(Request.Form["Id"]);
            itemInfo.ItemCategory = Request.Form["ItemCategory"];
            itemInfo.ItemName = Request.Form["ItemName"];
            itemInfo.ItemDescription = Request.Form["ItemDescription"];
            itemInfo.ItemThumbnail = Request.Form["ItemThumbnail"];
            itemInfo.ItemPrice = Request.Form["ItemPrice"];
            string sourcePath = @"D:\TRAVAUX ADO .NET\Images Projet";
            string targetPath = @"D:\TRAVAUX ADO .NET\Projet ADO\Arts\Arts\Arts\wwwroot\UploadImage\";
            CopyThumbail copyThumbail = new CopyThumbail();
            copyThumbail.CopyThumbnail(itemInfo.ItemThumbnail, sourcePath, targetPath);

            try
            {
                string connectionString = "Data Source=.;Initial Catalog=Arts;Integrated Security=True";

                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE Items " +
                                 "SET ItemCategory= @ItemCategory, ItemName= @ItemName, ItemDescription= @ItemDescription, ItemThumbnail= @ItemThumbnail, ItemPrice= @ItemPrice " +
                                 "WHERE Id= @Id;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", itemInfo.Id);
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
                return;
            }
        }
    }
}
