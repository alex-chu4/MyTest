using EMIC2.Models.Dao.Dto.HEALTH_DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMIC2.Models.Interface.HEALTH_DB
{
    public interface IHealthDao
    {
        void Insert(HEALTH_HIS2Dto model);
    }
}
