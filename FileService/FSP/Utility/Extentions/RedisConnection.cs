using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Extentions
{
    public sealed class RedisConnection
    {
        private static Lazy<RedisConnection> lazy = new Lazy<RedisConnection>(() =>
        {
            if (String.IsNullOrEmpty(_settingOption)) throw new InvalidOperationException("Please call Init() first.");
            return new RedisConnection();
        });

        private static string _settingOption;

        public readonly ConnectionMultiplexer ConnectionMultiplexer;

        public static RedisConnection Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        private RedisConnection()
        {
            try
            {
                ConnectionMultiplexer = ConnectionMultiplexer.Connect(_settingOption);
            } catch(Exception ex) { }
        }

        public static void Init(string settingOption)
        {
            _settingOption = settingOption;
        }
    }
}
