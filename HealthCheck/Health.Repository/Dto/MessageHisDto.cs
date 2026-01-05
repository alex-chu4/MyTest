using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Repository.Dto
{
    public class MessageHisDto
    {
        public string MESSAGE_TYPE { get; set; }

        public string MESSAGE_CONTENT { get; set; }

        public string MESSAGE_HASH { get; set; }

        public DateTime CREATE_TIME { get; set; }
    }
}
