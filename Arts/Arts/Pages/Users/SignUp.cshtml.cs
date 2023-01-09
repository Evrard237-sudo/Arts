using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Arts.Pages.Users
{
    public class SignUpModel : PageModel
    {
        public UserInfo userInfo = new UserInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            // Meme proceder que les autres
            userInfo.Username = Request.Form["Username"];
            userInfo.Useremail = Request.Form["Useremail"];
            userInfo.Userpassword = Request.Form["Userpassword"];

            // Toujours le meme
            if (userInfo.Username.Length == 0 || userInfo.Useremail.Length == 0
                || userInfo.Userpassword.Length == 0)
            {
                errorMessage = "All the fields are required !!!";
                return;
            }

            // Sauvergader le client dans la BD
         
            try
            {
                // Chaine de conection
                string ConnectioString = "Data Source=.;Initial Catalog=Arts;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(ConnectioString))
                {
                    connection.Open();
                    // Requete sql permettant d' inserer une valeur dans la base de données
                    string sql = "INSERT INTO Users " +
                                 "(Username, Useremail, Userpassword) VALUES " +
                                 "(@Username, @Useremail, @Userpassword);";
                    // Les valeurs des parametres 
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@Username", userInfo.Username);
                        cmd.Parameters.AddWithValue("@Useremail", userInfo.Useremail);
                        cmd.Parameters.AddWithValue("@Userpassword", userInfo.Userpassword);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            // Si tout ne se passe pas bien alors une exeption se leve
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            userInfo.Username = "";
            userInfo.Useremail = "";
            userInfo.Userpassword = "";
            successMessage = "You are correctly sign up !!!";

            // On se redirige vers la page login
            Response.Redirect("LogIn");
        }
    }
}
