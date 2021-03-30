using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GD.FinishingSystem.WEB.Classes
{
    public static class Extensions
    {
        public static bool IsNumeric(this object obj) => obj != null && double.TryParse(obj.ToString(), out _);
    }
}
