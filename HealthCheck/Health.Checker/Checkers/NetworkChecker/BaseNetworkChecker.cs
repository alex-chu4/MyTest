using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Health.Checker.Interfaces;

namespace Health.Checker.Checkers.NetworkChecker
{
    public abstract class BaseNetworkChecker<T> : INetworkChecker<T>
    {
        protected Stopwatch stopwatch = new Stopwatch();

        public Stopwatch Stopwatch { get { return stopwatch; } }

        protected abstract Task<T> CheckFuncAsync(string requestUri, int timeout);

        public async Task<T> CheckAsync(string requestUri, int timeout)
        {
            T t = default(T);

            stopwatch.Start();
            t = await CheckFuncAsync(requestUri, timeout);
            stopwatch.Stop();

            return t;
        }

        protected abstract T CheckFunc(string requestUri, int timeout);

        public T Check(string requestUri, int timeout)
        {
            T t = default(T);

            stopwatch.Start();
            t = CheckFunc(requestUri, timeout);
            stopwatch.Stop();

            return t;
        }
    }
}
