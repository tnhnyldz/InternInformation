using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternInformation.Helper
{
    public static class HelperClass3
    {
        //iş günü ekleyen metot
        public static System.DateTime AddBusinessDays(System.DateTime source, int businessDays)
        {
            var dayOfWeek = businessDays < 0
                                ? ((int)source.DayOfWeek - 12) % 7
                                : ((int)source.DayOfWeek + 6) % 7;

            switch (dayOfWeek)
            {
                case 6:
                    businessDays--;
                    break;
                case -6:
                    businessDays++;
                    break;
            }

            return source.AddDays(businessDays + ((businessDays + dayOfWeek) / 5) * 2);
        }
        public static DateTime AddBusinessDays2(DateTime dt, int nDays)
        {
            int weeks = nDays / 5;
            nDays %= 5;
            while (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
                dt = dt.AddDays(1);
            while (nDays-- > 0)
            {
                dt = dt.AddDays(1);
                if (dt.DayOfWeek == DayOfWeek.Saturday)
                    dt = dt.AddDays(2);
            }
            return dt.AddDays(weeks * 7);
        }
    }
}