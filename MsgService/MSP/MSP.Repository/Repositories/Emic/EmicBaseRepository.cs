using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSP.Repository.Repositories.Emic
{
    public class EmicBaseRepository : BaseRepository
    {
        public string ConnectionString
        {
            get
            {
                return GetConnectionString("EMICDB");
            }
        }
    }
}
