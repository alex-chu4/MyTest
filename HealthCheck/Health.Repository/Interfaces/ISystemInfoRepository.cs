using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Health.Repository.Dto;
using System.Linq;
using System.Text;

namespace Health.Repository.Interfaces
{
    public interface ISystemInfoRepository
    {
        Task<IEnumerable<SystemInfoDto>> Search(SystemInfoSearchFilter filter);

        Task<int> Create(SystemInfoDto systemInfo);

        Task<int> Delete(SystemInfoDto systemInfo);

        Task<int> Update(SystemInfoDto systemInfo);

        Task<int> UpdateThreshold(string SYSTEM_ID, float THRESHOLD);
    }
}
