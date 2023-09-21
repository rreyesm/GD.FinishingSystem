﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GD.FinishingSystem.Entities
{
    [Table("tblMigrationCategories")]
    public class MigrationCategory: BaseEntity
    {
        [Key]
        public int MigrationCategoryID { get; set; }
        public string Name { get; set; }

    }
}
