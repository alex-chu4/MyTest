using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MSP.Repository.Models.Emic;

namespace MSP.Repository.Interfaces.Emic
{
    public interface IMspLicenseRepository
    {
        SYS_MSP_LICENSE SearchByOid(string oid);
    }
}
