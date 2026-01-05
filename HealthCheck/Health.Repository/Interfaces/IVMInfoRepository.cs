using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Health.Repository.Dto;

namespace Health.Repository.Interfaces
{
    public interface IVMInfoRepository
    {
        Task<int> Create(VMInfoDto vmInfo);

        Task<int> Delete(VMInfoDto vmInfo);

        Task<int> Update(VMInfoDto vmInfo);

        Task<IEnumerable<VMInfoDto>> Search(VMInfoSearchFilter filter);
    }
}
