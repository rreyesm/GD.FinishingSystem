using System;

namespace GD.FinishingSystem.WEB
{
    public static class SystemStatics
    {
        public const string DefaultScheme = "GDFinishingSystemScheme";

        public static bool IsBetween(this DateTime selectedDate, DateTime dtBegin, DateTime dtEnd) =>
            (selectedDate <= dtEnd && selectedDate >= dtBegin) || (selectedDate <= dtBegin && selectedDate >= dtEnd);

    }
}
