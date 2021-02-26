using GD.FinishingSystem.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Bussines.Abstract
{
    public abstract class AbstractFloorService
    {
        public abstract Task<IEnumerable<Floor>> GetFloorList();
        public abstract Task<Floor> GetFloorFromFloorID(int FloorID);
        public abstract Task Add(Floor floor, int adderRef);
        public abstract Task Update(Floor floor, int updaterRef);
        public abstract Task Delete(Floor floor, int deleterRef);
    }
}
