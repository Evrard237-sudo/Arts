using Arts.Pages.Carts;
using System.Data.SqlClient;

namespace Arts.Services
{
    public class CountNbreItem
    {
        public int CountItems(int IdUser)
        {
            int count = 0;

            try
            {
                string connectionString = "Data Source=.;Initial Catalog=Arts;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string script = "SELECT COUNT(Id) FROM Carts WHERE id_user = " + IdUser;
                    using (SqlCommand command = new SqlCommand(script, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.Read())
                                count = reader.GetInt32(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return count;
        }
    }
}
