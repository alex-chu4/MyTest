using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Model;

namespace LSP.API
{
    public static class LogQueue
    {
        public static ConcurrentQueue<LogQueueDataModel> logQueue = new ConcurrentQueue<LogQueueDataModel>();
    }
}
