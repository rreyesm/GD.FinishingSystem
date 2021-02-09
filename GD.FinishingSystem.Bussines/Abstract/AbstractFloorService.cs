using GD.FinishingSystem.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Bussines.Abstract
{
    public abstract class AbstractFloorService
    {
        public abstract Task<IEnumerable<Floor>> GetRuloList();
        public abstract Task<Floor> GetRuloFromRuloID(int floorID);
        public abstract Task Add(Floor floorInformation);
        public abstract Task Update(Floor floorInformation);
        public abstract Task Delete(Floor floorInformation);
    }
}
