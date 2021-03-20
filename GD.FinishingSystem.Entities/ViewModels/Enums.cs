using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace GD.FinishingSystem.Entities.ViewModels
{
    public enum OriginType
    {
        Process = 1,
        [Description("Inspection Reprocess")]
        InspectionReprocess = 2,
        [Description("Internal Reprocess")]
        InternalReprocess = 3,
        [Description("Quality Recovery")]
        QualityRecovery = 4
    }
}
