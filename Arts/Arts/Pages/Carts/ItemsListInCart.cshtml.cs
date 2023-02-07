using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Arts.Pages.Carts
{
    public class ItemsListInCartModel : PageModel
    {
        public List<Models.Carts> CartList = new List<Models.Carts>();
        public int? IsAdmin { get; set; }
        public string? ReturnUsername { get; set; }
        public int? ReturnId { get; set; }
        public void OnGet()
        {
            ReturnId = int.Parse(s: HttpContext.Session.GetString("IdUser"));
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
                string connectionString = "Data Source=.;Initial Catalog=Arts;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT c.id_items, i.ItemName, i.ItemThumbnail, c.id_user, i.ItemPrice " +
                         "FROM Items i " +
                         "JOIN Carts c " +
                         "ON(i.Id = c.id_items) " +
                         "WHERE c.id_user = @id_user; ";
                    string sql2 = "SELECT SUM(ItemPrice) " +
                        "FROM Items i " +
                        "JOIN Carts c " +
                        "ON i.Id = c.id_items " +
                        "WHERE c.id_user = @id_user; ";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id_user", ReturnId);
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Models.Carts cart = new Models.Carts();
                                cart.id_item = reader.GetInt32(0);
                                cart.name = reader.GetString(1);
                                cart.thumnail = reader.GetString(2);
                                cart.id_user = reader.GetInt32(3);
                                cart.price = reader.GetDouble(4);

                                CartList.Add(cart);
                            }
                        }

                    }

                    using (SqlCommand command2 = new SqlCommand(sql2, connection))
                    {
                        command2.Parameters.AddWithValue("@id_user", ReturnId);
                        using (SqlDataReader reader2 = command2.ExecuteReader())
                        {
                            if (reader2.Read())
                            {
                                HttpContext.Session.SetString("total", reader2.GetDouble(0).ToString());
                                string test = HttpContext.Session.GetString("total");
                                Console.WriteLine(test);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
