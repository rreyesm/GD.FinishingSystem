using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace GD.FinishingSystem.WEB.Classes
{
    public static class Extensions
    {
        public static bool IsNumeric(this object obj) => obj != null && double.TryParse(obj.ToString(), out _);

        public static DateTime AccountStartDate(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 7, 0, 0);
        }

        public static DateTime AccountEndDate(this DateTime dateTime)
        {
            dateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 7, 0, 0);
            return dateTime.AddTicks(-1);
        }

        public static DateTime RealDateStartDate(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);
        }

        public static DateTime RealDateEndDate(this DateTime dateTime)
        {
            dateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
            return dateTime.AddDays(1).AddTicks(-1); ;
        }

    }
}
