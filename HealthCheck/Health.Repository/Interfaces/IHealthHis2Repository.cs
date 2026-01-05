using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Health.Repository.Dto;

namespace Health.Repository.Interfaces
{
    public interface IHealthHis2Repository
    {
        Task<IEnumerable<HealthHis2Dto>> Search(HealthHis2SearchFilter filter);

        Task<int> UpdateThreshold(HealthHis2Dto healthHis2);
    }
}
