using GD.FinishingSystem.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Entities
{
    [Table("tblPackingList")]
    public class PackingList : BaseEntity
    {
        public int PackingListID { get; set; }
        public PackingListType PackingListType { get; set; }
        public int PackingListNo { get; set; }
    }
}
