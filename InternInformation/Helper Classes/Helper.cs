using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternInformation.Helper_Classes
{
    public class Helper
    {
        public string generatePassword()
        {
            Random rnd = new Random(); 
            int password1 = rnd.Next(100,1000);
            int password2 = rnd.Next(100,1000);
            string WholePassword=password1.ToString()+password2.ToString();
            return WholePassword;
            
        }
    }
}