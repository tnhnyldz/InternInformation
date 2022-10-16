using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class Helper2
    {
        public string stajDeğişiklik(string stdntName, string stdntMail)
        {
            MailMessage eposta = new MailMessage();
            eposta.From = new MailAddress("KOUSTAJ2022@outlook.com");
            eposta.To.Add(stdntMail);
            eposta.Subject = "Staj Durumu Değişikliği !";
            eposta.Body = "Sayın "+ stdntName+ " Staj durumunuzda bir güncelleme var lütfen sisteme giriniz";

            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new System.Net.NetworkCredential("KOUSTAJ2022@outlook.com", "Yazlab2022.");
            smtp.Host = "smtp.outlook.com";
            smtp.EnableSsl = true;
            smtp.Port = 587;

            smtp.Send(eposta);
            Console.WriteLine("gonderıldı");
            return "deneme";
        }


    }
}
