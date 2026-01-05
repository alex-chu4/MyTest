using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Checker.Interfaces
{
    public interface INetworkChecker<T>
    {
        Stopwatch Stopwatch { get; }

        Task<T> CheckAsync(string requestUri, int timeout);

        T Check(string requestUri, int timeout);
    }
}
