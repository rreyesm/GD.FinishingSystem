using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GD.FinishingSystem.Entities
{
    [Table("tblMigrationControls")]
    public class MigrationControl
    {
        public int MigrationControlId { get; set; }
        [Display(Name = "ExcelFilePath")]
        public string ExcelFilePath { get; set; }
        [Display(Name = "File Name")]
        public string FileName { get; set; }
        [Display(Name = "Last Migrated Row Of Excel File")]
        public int LastMigratedRowOfExcelFile { get; set; }
        [Display(Name = "File Rows Total")]
        public int FileRowsTotal { get; set; }
        [Display(Name = "Rows Total Migrated")]
        public int RowsTotalMigrated { get; set; }
        public string ErrorMesage { get; set; }
        [Display(Name = "Begin Date")]
        public DateTime BeginDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }


    }
}
