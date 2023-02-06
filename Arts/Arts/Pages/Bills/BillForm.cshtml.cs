using Arts.Pages.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Primitives;
using System.Data.SqlClient;

namespace Arts.Pages.Bills
{
    public class BillFormModel : PageModel
    {
        public string success = "";
        public string? ReturnUsername { get; set; }
        public int? IsAdmin { get; set; }
        public Models.UserInfo userInfo = new Models.UserInfo();
        public Models.Bills bill = new Models.Bills();
        public void OnGet()
        {
            ReturnUsername = HttpContext.Session.GetString("username");

            try
            {
                string ConnectionString = "Data Source=.;Initial Catalog=Arts;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Users WHERE Username = '" + ReturnUsername + "';";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                userInfo.Id = "" + reader.GetInt32(0);
                                userInfo.Username = reader.GetString(1);
                                userInfo.Useremail = reader.GetString(2);
                                userInfo.Userpassword = reader.GetString(3);
                                userInfo.IsAdmin = "" + reader.GetInt32(4);
                                userInfo.CreateAt = reader.GetDateTime(5).ToString();
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

        public void OnPost()
        {
            bill.PaymentMode = Request.Form["payment"];
            bill.TotalPrice = HttpContext.Session.GetString("total");
            bill.Country = Request.Form["country"];
            bill.City = Request.Form["city"];
            bill.CardName = Request.Form["creditCard"];
            bill.CardNumber = Request.Form["cardNumber"];
            bill.Adress = Request.Form["adress"];
            bill.DetailAdress = Request.Form["detail"];
            bill.id_user = int.Parse(s: HttpContext.Session.GetString("IdUser")); ;
            
            try
            {
                string connsectionString = "Data Source=.;Initial Catalog=Arts;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connsectionString))
                {
                    connection.Open();

                    string script = "INSERT INTO Bills (PaymentMode, TotalPrice, Country, City, CardName, CardCreditNumber, Adress, DetailAdress, id_user)" +
                                    "VALUES (@PaymentMode, @TotalPrice, @Country, @City, @CardName, @CardCreditNumber, @Adress, @DetailAdress, @id_user);";

                    using (SqlCommand command = new SqlCommand(script, connection))
                    {
                        command.Parameters.AddWithValue("@PaymentMode", bill.PaymentMode);
                        command.Parameters.AddWithValue("@TotalPrice", bill.TotalPrice);
                        command.Parameters.AddWithValue("@Country", bill.Country);
                        command.Parameters.AddWithValue("@City", bill.City);
                        command.Parameters.AddWithValue("@CardName", bill.CardName);
                        command.Parameters.AddWithValue("@CardCreditNumber", bill.CardNumber);
                        command.Parameters.AddWithValue("@Adress", bill.Adress);
                        command.Parameters.AddWithValue("@DetailAdress", bill.DetailAdress);
                        command.Parameters.AddWithValue("@id_user", bill.id_user);

                        command.ExecuteNonQuery();
                        Console.WriteLine("Bill successfully created !!!");
                        success = "Succes";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Response.Redirect("/Error404");
            }
        }
    }
}
