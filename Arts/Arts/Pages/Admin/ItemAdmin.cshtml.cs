using Arts.Pages.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Arts.Pages.Admin
{
    public class ItemAdminModel : PageModel
    {
        public List<ItemInfo> ListItem = new List<ItemInfo>();
        public void OnGet()
        {
            try
            {
                string ConnectionString = "Data Source=.;Initial Catalog=Arts;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                { // Connection a la BD
                    connection.Open();
                    // REquete sql permettant d' afficher la liste des utilisateurs
                    string sql = "SELECT * FROM Items";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {// La lecture des donnees 
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ItemInfo item = new ItemInfo();
                                item.Id = reader.GetInt32(0);
                                item.ItemCategory = reader.GetString(1);
                                item.ItemName = reader.GetString(2);
                                item.ItemDescription = reader.GetString(3);
                                item.ItemThumbnail = reader.GetString(4);
                                item.ItemPrice = reader.GetDouble(5).ToString();
                                item.CreateAt = reader.GetDateTime(6).ToString();

                                ListItem.Add(item);
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

    public class ItemInfo
    {
        public int Id;
        public string? ItemCategory;
        public string? ItemName;
        public string? ItemDescription;
        public string? ItemThumbnail;
        public string? ItemPrice;
        public string? CreateAt ;
    }
}
