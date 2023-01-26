using Arts.Pages.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Arts.Pages.Items
{
    public class ListItemModel : PageModel
    {
        public int? IsAdmin { get; set; }
        public List<ItemInfo> ListItem = new List<ItemInfo>();
        public string? ReturnUsername { get; set; }
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

        public void OnPost()
        {
            ReturnUsername = "";
            HttpContext.Session.Remove("username");
            Response.Redirect("/Index");
        }
    }
}
