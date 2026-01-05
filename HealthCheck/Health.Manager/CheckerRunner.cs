using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using AutoMapper;
using Health.Checker.Checkers.NetworkChecker;
using Health.Repository.Dto;
using Health.Service.Interfaces;

namespace Health.Manager
{
    class CheckerRunner
    {
        private const double MS2S = 1000.0;

        private readonly HealthTargetDto healthTarget;
        private readonly IMapper mapper;
        private readonly IHealthService service;

        static int PingTimeout
        {
            get
            {
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["PingTimeout"]))
                    return Convert.ToInt32(ConfigurationManager.AppSettings["PingTimeout"]);
                else
                    return 60000;
            }
        }

        public CheckerRunner(HealthTargetDto healthTarget, IMapper mapper, IHealthService service)
        {
            this.healthTarget = healthTarget;
            this.mapper = mapper;
            this.service = service;
        }

        public async Task RunAsync()
        {
            bool result;

            HealthCurrentDto healthCurrent = mapper.Map<HealthTargetDto, HealthCurrentDto>(healthTarget);
            HealthHisDto healthHis = mapper.Map<HealthTargetDto, HealthHisDto>(healthTarget);
            string url = healthTarget.URL;

            if (url.ElementAt(url.Length - 1) != '/')
                url += '/';

            healthCurrent.STATUS = healthHis.STATUS = result = await SendNECAgentAsync(url, healthHis);
            if (result)
            {
                healthCurrent.STATUS = healthHis.STATUS = await SendCitrixAgentAsync(url, healthHis);
            }

            await service.InsertOrUpdateCurrentAsync(healthCurrent);
            await service.InsertHisAsync(healthHis);
        }

        private async Task<bool> SendNECAgentAsync(string url, HealthHisDto healthHis)
        {
            HttpChecker httpChecker = new HttpChecker();

            using (HttpResponseMessage response = await httpChecker.CheckAsync(url + "Health/NECAgent", PingTimeout))
            {
                if (response.IsSuccessStatusCode)
                {
                    healthHis.SYSTEM_TIME = Convert.ToSingle(Convert.ToDouble(httpChecker.Stopwatch.ElapsedTicks) / Stopwatch.Frequency);
                }

                return response.IsSuccessStatusCode;
            }
        }

        private async Task<bool> SendCitrixAgentAsync(string url, HealthHisDto healthHis)
        {
            HttpChecker httpChecker = new HttpChecker();

            using (HttpResponseMessage response = await httpChecker.CheckAsync(url + "Health/CitrixAgent", PingTimeout))
            {
                if (response.IsSuccessStatusCode)
                {
                    string dbTime = await response.Content.ReadAsStringAsync();
                    if (dbTime == "-1") return false;

                    healthHis.TOTAL_TIME = healthHis.SYSTEM_TIME = Convert.ToSingle(Convert.ToDouble(httpChecker.Stopwatch.ElapsedTicks) / Stopwatch.Frequency);
                    healthHis.DB_TIME = Convert.ToSingle(Convert.ToSingle(dbTime) / MS2S);
                }

                return response.IsSuccessStatusCode;
            }
        }

        public void Run()
        {
            RunAsync().Wait();
        }
    }
}
