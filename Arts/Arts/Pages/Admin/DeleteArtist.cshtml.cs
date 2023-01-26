using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Arts.Pages.Admin
{
    public class DeleteArtistModel : PageModel
    {
        public void OnGet()
        {
            int Id = int.Parse(Request.Query["Id"]);

            try
            {
                string connectionString = "Data Source=.;Initial Catalog=Arts;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "DELETE FROM Artists WHERE Id = @Id;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", Id);

                        command.ExecuteNonQuery();
                    }
                }

                Response.Redirect("/Admin/ArtistAdmin");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
    }
}
