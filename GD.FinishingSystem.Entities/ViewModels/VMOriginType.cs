using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GD.FinishingSystem.Entities.ViewModels
{
    public class VMOriginType
    {
        public string Text { get; set; }
        public int Value { get; set; }

        public static List<VMOriginType> ToList()
        {
            var originList = Enum.GetValues(typeof(OriginType)).Cast<IFormattable>().Select(x => new VMOriginType
            {
                Text = x.ToString().SplitCamelCase(),
                Value = int.Parse(x.ToString("d", null)),
            }).ToList();

            return originList;
        }
    }
}
