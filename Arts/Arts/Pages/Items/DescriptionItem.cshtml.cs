using Arts.Pages.Admin;
using Arts.Pages.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Arts.Pages.Items
{
    public class DescriptionItemModel : PageModel
    {
        public int? IsAdmin { get; set; }
        public string? ReturnUsername { get; set; }
        public int? ReturnId { get; set; }
        public Forum forum = new Forum();
        public List<Forum> ListForum = new List<Forum>();
        public ItemInfo itemInfo = new ItemInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            ReturnUsername = HttpContext.Session.GetString("username");
            int Id = int.Parse(Request.Query["Id"]);
            Console.WriteLine(Id.ToString());
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

                /*
                 * Cette fonction chargera en premier temps l' item selectionner et en duxieme temps 
                 * la liste des commentaires effectuer sur cette items
                 */
                string connectionSring = "Data Source=.;Initial Catalog=Arts;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionSring))
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
                            }
                        }
                    }
                }
                try
                {
                    using(SqlConnection connection = new SqlConnection(connectionSring))
                    {
                        connection.Open();
                        string sql2 = "SELECT f.Id, f.ForumComment, f.CreateAt, f.id_user, f.id_item, u.Username FROM Forums f JOIN Users u ON (u.Id = f.id_user) WHERE id_item = @id_Item;";
                        // Requete de rechange :
                        // SELECT * FROM Forums WHERE id_item = @id_Item
                        //string sql3 = "SELECT * FROM Users WHERE Id= @Id";
                        using (SqlCommand command2 = new SqlCommand(sql2, connection))
                        {
                            // la valeur de l' id de l' item
                            command2.Parameters.AddWithValue("@id_Item", Id);
                            using (SqlDataReader reader2 = command2.ExecuteReader())
                            {
                                while (reader2.Read())
                                {
                                    forum.Id = reader2.GetInt32(0);
                                    forum.ForumComment = reader2.GetString(1);
                                    forum.CreateAt = reader2.GetDateTime(2).ToString();
                                    forum.IdUser = reader2.GetInt32(3);
                                    forum.IdItem = reader2.GetInt32(4);
                                    forum.profile = reader2.GetString(5);
                                    Console.WriteLine(forum.profile);

                                    ListForum.Add(forum);
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
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            forum.ForumComment = Request.Form["ForumComment"];
            int IdItem = int.Parse(Request.Query["Id"]);
            ReturnId = int.Parse(s: HttpContext.Session.GetString("IdUser"));
            Console.WriteLine(ReturnId);
            // Sauvegarder le message dans la bd
            try
            {
                string connectionString = "Data Source=.;Initial Catalog=Arts;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Forums " +
                                 "(ForumComment, id_user, id_item) VALUES " +
                                 "(@ForumComment, @id_user, @id_item);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ForumComment", forum.ForumComment);
                        command.Parameters.AddWithValue("@id_user", ReturnId);
                        command.Parameters.AddWithValue("@id_item", IdItem);

                        command.ExecuteNonQuery();
                    }
                }
                successMessage = "Message successfully send";
                Response.Redirect("/Items/ListItem");
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
    }

    public class Forum
    {
        public int Id;
        public string? ForumComment;
        public string? CreateAt;
        public int IdUser;
        public int IdItem;
        public string? profile;
    }
}
