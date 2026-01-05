using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Health.Repository.Dto;

namespace Health.Repository.Interfaces
{
    public interface IHealthCurrentRepository
    {
        Task<IEnumerable<HealthCurrentViewDto>> GetCurrentView();

        Task<bool> IsExist(HealthCurrentDto healthCurrent);

        Task Insert(HealthCurrentDto healthCurrent);

        Task Update(HealthCurrentDto healthCurrent);
    }
}
