using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace GD.FinishingSystem.Entities.ViewModels
{
    public enum OriginType
    {
        //Process
        [Description("PP00")]
        PP00 = 1,
        //Inspection Process and Internal Process
        [Description("PF00")]
        PF00 = 2,
        //Quality Recovery
        [Description("TF02")]
        TF02 = 3,
        //Quality Recovery
        [Description("DVF2")]
        DVF2 = 4,
        //From inspection stock, integration stock
        [Description("PF01")]
        PF01 = 5,
        //From inspection stock or integration stock
        [Description("PF02")]
        PF02 = 6,
        //For factory tests
        [Description("DES0")]
        DES0 = 7
    }
}
