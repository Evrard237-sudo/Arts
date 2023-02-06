//using System.Web;
//using System.Net.Mail;
////using iTextSharp.text.pdf;
////using iTextSharp.text.html;
//using System.IO;
//using System.Text.RegularExpressions;
////using iTextSharp.text.html.simpleparser;
//using Microsoft.AspNetCore.Hosting.Server;
//using System.Diagnostics;
////using iTextSharp.text;

//namespace Arts.Services
//{
//    public class GestionFacture
//    {
//        public void EmailSender(string UserEmail, string body) 
//        {// Ceci est une fonctionnalité commencé mais pas fini<
//            // Il y' a des erreurs dont j' en ai encore aucune idée
//			/*try
//			{
//                MailMessage mail = new MailMessage("lenked43@gmail.com", UserEmail, "Bill", body);
//                SmtpClient client = new SmtpClient("smtp.gmail.com");
//                client.Port = 587;
//                client.Credentials = new System.Net.NetworkCredential("lenked43@gmail.com", "naruto16");
//                client.EnableSsl = true;
//                client.Send(mail);
//                Console.WriteLine("Message successfuly sended !!!");
//            }
//			catch (Exception ex)
//			{
//                Console.WriteLine(ex.Message);
//			}*/
//        }

//        public void GenererFacture()
//        {
//            ///*var doc1 = new Document();
//            //string path = Server.MapPath("PDFs");
//            //PdfWriter.GetInstance(doc1, new FileStream(path + "/Doc1.pdf", FileMode.Create));
//            //doc1.Open();
//            //doc1.Add(new Paragraph("My first PDF"));
//            //doc1.Close();*/
//            //string outFile = Environment.CurrentDirectory + "/Bill.pdf";
//            //// Création du document
//            //Document doc = new Document();
//            //PdfWriter.GetInstance(doc, new FileStream(outFile, FileMode.Create));
//            //doc.Open();

//            //// palette de couleurs
//            //BaseColor blue = new BaseColor(0, 75, 155);
//            //BaseColor gris = new BaseColor(240, 240, 240);
//            //BaseColor blanc = new BaseColor(255, 255, 255);

//            //// Polices d' écriture
//            //Font policeTitre = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 20f, iTextSharp.text.Font.BOLD, blue);
//            //Font policeTh = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16f, iTextSharp.text.Font.BOLD, blanc);

//            // Page 
//            // Creation de paragraphes
//            //Paragraph p1 = new Paragraph("Test");
//            //p1.Alignment = Element.ALIGN_CENTER;
//            //doc.Add(p1);

//            ////Paragraph p2 = new Paragraph("Le client, " + client.Text + "\n", policeTitre);
//            ////p2.Alignment = Element.ALIGN_RIGHT;
//            ////doc.Add(p2);

//            ////Paragraph p3 = new Paragraph("Devis, " + devis.Text + "\n\n", policeTitre);
//            ////p3.Alignment = Element.ALIGN_LEFT;
//            ////doc.Add(p3);

//            //// Creation du tableau des produits
//            //PdfPTable pdfPTable = new PdfPTable(3);
//            //pdfPTable.WidthPercentage = 100;

//            //// Cellule du tableau
//            ////AddCellToTab("Designation", policeTh, blue, pdfPTable);
//            ////AddCellToTab("Quantité", policeTh, blue, pdfPTable);
//            ////AddCellToTab("Prix", policeTh, blue, pdfPTable);

//            //// Lister les produits
//            //string[] InfosProduit = new string[3];
//            ////InfosProduit[0] = nom.Text;
//            ////InfosProduit[1] = qte.Text;
//            ////InfosProduit[2] = prix.Text;

//            //foreach (String info in InfosProduit)
//            //{
//            //    PdfPCell cell = new PdfPCell(new Phrase(info));
//            //    cell.BackgroundColor = gris;
//            //    cell.Padding = 7;
//            //    cell.BorderColor = gris;
//            //    pdfPTable.AddCell(cell);
//            //}

//            //doc.Add(pdfPTable);
//            //doc.Add(new Phrase("\n"));

//            ////int prixTotal = int.Parse(prix.Text) * int.Parse(qte.Text);
//            ////Paragraph p4 = new Paragraph("Prix total :" + prixTotal, policeTh);
//            ////doc.Add(p4);

//             // Fermer le document
//            //doc.Close();
//            //Process.Start(@"cmd.exe ", @"/c " + outFile);

    
//        }
//    }
//}
