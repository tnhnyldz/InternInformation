using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace InternInformation.Helper
{
    public class HelperClass
    {
        //rastgele password olusturur
        public string generatePassword()
        {
            Random rnd = new Random();
            int password1 = rnd.Next(100, 1000);
            int password2 = rnd.Next(100, 1000);
            string WholePassword = password1.ToString() + password2.ToString();
            return WholePassword;

        }
        //Mail atma olayı
        public void SendMailPassword(string mail, string sifre)
        {

            MailMessage eposta = new MailMessage();
            eposta.From = new MailAddress("KOUSTAJ2022@outlook.com");
            eposta.To.Add(mail);
            eposta.Subject = "KOU Staj Sistemi şifreniz";
            eposta.Body = "KOU Staj Sistemine Kaydoldunuz şifreniz: " + sifre;

            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new System.Net.NetworkCredential("KOUSTAJ2022@outlook.com", "Yazlab2022.");
            smtp.Host = "smtp.outlook.com";
            smtp.EnableSsl = true;
            smtp.Port = 587;

            smtp.Send(eposta);
            Console.WriteLine("gonderıldı");
        }
        string hash = "";
        //şifreler
        public string encrypt(string value)
        {
            byte[] data = Encoding.Default.GetBytes(value);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(Encoding.Default.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripleDES.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return Convert.ToBase64String(results, 0, results.Length);
                }
            }
        }
        //şifre çözer
        public string decrypt(string value)
        {
            byte[] data = Convert.FromBase64String(value);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(Encoding.Default.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripleDES.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return Encoding.Default.GetString(results);
                }
            }
        }
        //iş Günü Hesaplama
        public static double GetBusinessDays(DateTime startD, DateTime endD)
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