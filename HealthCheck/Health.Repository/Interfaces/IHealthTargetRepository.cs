using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Health.Repository.Dto;

namespace Health.Repository.Interfaces
{
    public interface IHealthTargetRepository
    {
        Task<IEnumerable<HealthTargetDto>> GetActiveTarget();

        Task<int> Create(HealthTargetDto healthTarget);

        Task<int> Delete(HealthTargetDto healthTarget);

        Task<int> Update(HealthTargetDto healthTarget);

        Task<IEnumerable<HealthTargetDto>> Search(HealthTargetSearchFilter filter);
    }
}
