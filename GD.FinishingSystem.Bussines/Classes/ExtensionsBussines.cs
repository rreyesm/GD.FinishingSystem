using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Bussines.Classes
{
    public static class ExtensionsBussines
    {
        public static DateTime GetCurrentAccountingDate(this DateTime? dateTime)
        {
            return DateTime.Now.Hour < 7 ? DateTime.Today.AddDays(-1) : DateTime.Today;
        }

        public static DateTime? GetAccountingDate(this DateTime dateTime)
        {
            return dateTime.Hour < 7 ? dateTime.AddDays(-1).Date : dateTime.Date;
        }

        public static DateTime GetRealDate(this DateTime dateTime, bool isEndDate)
        {
            if (isEndDate)
            {
                dateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 7, 0, 0);
                return dateTime.AddDays(1).AddTicks(-1);
            }
            else
            {
                dateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 7, 0, 0);
                return dateTime;
            }

        }
    }
}
