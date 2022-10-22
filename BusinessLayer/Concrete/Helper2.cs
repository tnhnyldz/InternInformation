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
        //durum degısıklık maılı
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
        //iş günü hesaplayıcı
        public double GetBusinessDays(DateTime startD, DateTime endD)
        {
            double calcBusinessDays =
                1 + ((endD - startD).TotalDays * 5 -
                (startD.DayOfWeek - endD.DayOfWeek) * 2) / 7;

            if (endD.DayOfWeek == DayOfWeek.Saturday) calcBusinessDays--;
            if (startD.DayOfWeek == DayOfWeek.Sunday) calcBusinessDays--;

            return calcBusinessDays;
        }

    }
}
