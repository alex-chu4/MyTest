using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Health.Repository.Dto;

namespace Health.Repository.Interfaces
{
    public interface IHealthHisRepository
    {
        Task Insert(HealthHisDto healthHis);

        Task<IEnumerable<HealthHisDto>> Search(HealthHisSearchFilter filter);
    }
}
