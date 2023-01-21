using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using Arts.Pages;

namespace Arts.Pages.Users
{
    public class LogInModel : PageModel
    {
        public UserInfo userInfo = new UserInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            // On relie les input au donnees de la liste UserInfo
            userInfo.Useremail = Request.Form["Useremail"];
            userInfo.Userpassword = Request.Form["Userpassword"];
            userInfo.Username = Request.Form["Username"];

            // Si les input sont vides unr=e erreur se declanche
            if (userInfo.Userpassword.Length == 0 ||
                userInfo.Userpassword.Length == 0 ||
                userInfo.Username.Length == 0)
            {
                errorMessage = "All fields are required !!!";
                return;
            }

            // Verification si l' utilisateur existe dans la BD
            try
            {
                // Chaine de connection
                string ConnectionString = "Data Source=.;Initial Catalog=Arts;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    // Requete sql permettant de verififer si l' utilisateur avec lequel on voudrait se connecter existe
                    string sql = "SELECT * FROM Users WHERE Useremail = '"+ userInfo.Useremail +"' AND Userpassword = '" + userInfo.Userpassword + "' AND Username = '" + userInfo.Username +"'";
                    using (SqlDataAdapter sda = new SqlDataAdapter(sql, connection))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            // Recherche des donnees de la table
                            sda.Fill(dt);
                            if (dt.Rows.Count == 1)
                            { // Si l' insertion a été retrouver il s' affiche un message de succes
                                successMessage = "Authentification complete successfully !!!";
                                Console.WriteLine(successMessage);
                                Console.WriteLine(userInfo.Username);
                                // Je cree une session qui vas recuperer la valeur du username
                                HttpContext.Session.SetString("username", userInfo.Username);
                                // On vas se rediriger vers la page Home 
                                Response.Redirect("/Index");
                            }
                            else
                            {
                                // Un message d' erreur s' affiche si l' utilisateur n' a pas été inserer
                                errorMessage = "Check your email and password.";
                                Console.WriteLine(errorMessage);
                                userInfo.Useremail = "";
                                userInfo.Userpassword = "";
                                userInfo.Username = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Si un probleme s' opere un message d' erreur est afficher
                errorMessage = ex.Message;
                return;
            }
        }
    }
}
