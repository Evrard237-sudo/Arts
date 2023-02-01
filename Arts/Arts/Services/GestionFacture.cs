using System.Web;
using System.Net.Mail;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using System.IO;
using System.Text.RegularExpressions;
using iTextSharp.text.html.simpleparser;
using Microsoft.AspNetCore.Hosting.Server;

namespace Arts.Services
{
    public class GestionFacture
    {
        public void EmailSender(string UserEmail, string body) 
        {// Ceci est une fonctionnalité commencé mais pas fini<
            // Il y' a des erreurs dont j' en ai encore aucune idée
			try
			{
                MailMessage mail = new MailMessage("lenked43@gmail.com", UserEmail, "Bill", body);
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Port = 587;
                client.Credentials = new System.Net.NetworkCredential("lenked43@gmail.com", "naruto16");
                client.EnableSsl = true;
                client.Send(mail);
                Console.WriteLine("Message successfuly sended !!!");
            }
			catch (Exception ex)
			{
                Console.WriteLine(ex.Message);
			}
        }

        public void GenererFacture()
        {
            /*var doc1 = new Document();
            string path = Server.MapPath("PDFs");
            PdfWriter.GetInstance(doc1, new FileStream(path + "/Doc1.pdf", FileMode.Create));
            doc1.Open();
            doc1.Add(new Paragraph("My first PDF"));
            doc1.Close();*/
        }
    }
}
