﻿using GD.FinishingSystem.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GD.FinishingSystem.Bussines.Abstract
{
    public abstract class AbstractPeriodService
    {
        public abstract Task<IEnumerable<Period>> GetPeriodList();
        public abstract Task<Period> GetPeriodFromPeriodID(int periodID);
        public abstract Task<Period> GetCurrentPeriod();
        public abstract Task Add(Period period, int adderRef);
        public abstract Task Update(Period period, int updaterRef);
        public abstract Task Delete(Period period, int deleterRef);
    }
}
