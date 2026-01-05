using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Health.Repository.Dto;

namespace Health.Repository.Interfaces
{
    public interface IFunctionRepository
    {
        Task<IEnumerable<FunctionDto>> Search(FunctionSearchFilter filter);
    }
}
