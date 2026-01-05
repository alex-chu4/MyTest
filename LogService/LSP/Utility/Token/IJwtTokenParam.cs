using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Model
{
    public interface IJwtTokenParam
    {
        string Issuer { get; set; }
        string Audience { get; set; }
        string KEY { get; set; }
    }
}
