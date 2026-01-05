using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MSP.Repository.Models.Emic;

namespace MSP.Repository.Interfaces.Emic
{
    public interface ICommonParameterRepository
    {
        SYS_COM_PARAM SearchByParamName(string paramName);
    }
}
