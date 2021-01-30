using System;
using System.Collections.Generic;
using System.Text;

namespace GD.FinishingSystem.Bussines
{
    internal static class StaticMetods
    {


        internal static bool IsBetweenDates(this DateTime selectedDate, DateTime dtBegin, DateTime dtEnd) =>
          (selectedDate <= dtEnd && selectedDate >= dtBegin) || (selectedDate <= dtBegin && selectedDate >= dtEnd);
    }
}
