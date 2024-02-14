using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GD.FinishingSystem.Entities.ViewModels
{
    [NotMapped]
    [Keyless]
    public class VMFinishedRawFabric : VMRuloMigrationReport
    {
        public string BatchNumbers { get; set; }
    }
}
