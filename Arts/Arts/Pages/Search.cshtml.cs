using Arts.Pages.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;

namespace Arts.Pages
{
    public class SearchModel : PageModel
    {
        public List<Models.ItemInfo> itemList = new List<Models.ItemInfo>();
        public List<Models.ArtistInfo> artistList = new List<Models.ArtistInfo>();
        public void OnGet()
        {
            string SearchingWord = Request.Query["s"];
			try
			{
                string ConnectionString = "Data Source=.;Initial Catalog=Arts;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string sql1 = "SELECT * FROM Items WHERE ItemCategory LIKE '%" + SearchingWord + "%' OR ItemName LIKE '%"+ SearchingWord +"%';";
                    string sql2 = "SELECT * FROM Artists WHERE ArtistName LIKE '%" + SearchingWord + "%';";
                    using(SqlCommand command = new SqlCommand(sql1, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read()){
                                Models.ItemInfo item = new Models.ItemInfo();
                                item.Id = reader.GetInt32(0);
                                item.ItemCategory = reader.GetString(1);
                                item.ItemName = reader.GetString(2);
                                item.ItemDescription = reader.GetString(3);
                                item.ItemThumbnail = reader.GetString(4);
                                item.ItemPrice = reader.GetDouble(5).ToString();
                                item.CreateAt = reader.GetDateTime(6).ToString();

                                itemList.Add(item);

                            }
                            reader.Close();
                        }
                    }

                    using (SqlCommand command = new SqlCommand(sql2, connection))
                    {
                        using (SqlDataReader r = command.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                Models.ArtistInfo artist = new Models.ArtistInfo();
                                artist.Id = r.GetInt32(0);
                                artist.ArtistName = r.GetString(1);
                                artist.ArtistEmail = r.GetString(2);
                                artist.ArtistUrl = r.GetString(3);
                                artist.ArtistThumbnail = r.GetString(4);

                                artistList.Add(artist);
                            }
                            r.Close();
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
