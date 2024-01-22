using GD.FinishingSystem.Entities;
using GD.FinishingSystem.Entities.ViewModels;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Bussines.Abstract
{
    public abstract class AbstractPackingListService
    {
        public abstract Task Add(PackingList packingList, int addRef);
        public abstract Task Update(PackingList packingList, int updaterRef);
        public abstract Task<PackingList> GetPackingList(int ruloMigrationID, PackingListType packingListType);
        public abstract int GetNextFromSequence(PackingListType packingListType);
    }
}
