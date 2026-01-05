using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Autofac;
using Health.Repository.Dto;
using Health.Service.Interfaces;
using NLog;

namespace Health.Notify
{
    class Program
    {
        static IContainer container;
        static ILogger logger = LogManager.GetCurrentClassLogger();

        static float DefaultThreshold
        {
            get
            {
                float threshold;

                if (!float.TryParse(ConfigurationManager.AppSettings["DefaultThreshold"], out threshold))
                {
                    threshold = 5.0f;
                }

                return threshold;
            }
        }

        static int EventTimeInterval
        {
            get
            {
                int interval;

                if (!int.TryParse(ConfigurationManager.AppSettings["EventTimeInterval"], out interval))
                {
                    interval = 60;
                }

                return interval;
            }
        }

        static void Main(string[] args)
        {
            try
            {
                RegisterAutofacConfig();

                IHealthService service = container.Resolve<IHealthService>();
                IEnumerable<NotificationInfoDto> notificationInfos = null;
                IEnumerable<HealthCurrentViewDto> healthCurrentViews = null;
                IEnumerable<EventDto> events = null;
                NotificationInfoSearchFilter notificationInfoSearchFilter =
                    new NotificationInfoSearchFilter()
                    {
                        ACTIVE = true
                    };

                if ((args.Count() > 0) && string.Compare(args[0], "async", true) == 0)
                {
                    Task.Run(async () =>
                    {
                        Parallel.For(0, 3, index =>
                        {
                            switch (index)
                            {
                                case 0:
                                    notificationInfos = service.SearchNotificationInfoAsync(notificationInfoSearchFilter).Result;
                                    break;
                                case 1:
                                    healthCurrentViews = service.GetCurrentViewAsync().Result;
                                    break;
                                case 2:
                                    events = service.GetHealthHis2OverThresholdAsync(EventTimeInterval, DefaultThreshold).Result;
                                    break;
                            }
                        });

                        if ((notificationInfos.Count() > 0) && ((healthCurrentViews.Count() > 0) || (events.Count() > 0)))
                        {
                            Alert alert = new Alert(service ,notificationInfos, healthCurrentViews, events);
                            await alert.SendAsync();
                        }
                    }).Wait();
                }
                else
                {
                    Parallel.For(0, 3, index =>
                    {
                        switch (index)
                        {
                            case 0:
                                notificationInfos = service.SearchNotificationInfo(notificationInfoSearchFilter);
                                break;
                            case 1:
                                healthCurrentViews = service.GetCurrentView();
                                break;
                            case 2:
                                events = service.GetHealthHis2OverThreshold(EventTimeInterval, DefaultThreshold);
                                break;
                        }
                    });

                    if ((notificationInfos.Count() > 0) && ((healthCurrentViews.Count() > 0) || (events.Count() > 0)))
                    {
                        Alert alert = new Alert(service, notificationInfos, healthCurrentViews, events);
                        alert.Send();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        static void RegisterAutofacConfig()
        {
            // === 1. 建立容器 ===
            var builder = new ContainerBuilder();

            // === 2. 註冊服務 ===
            // 取得目前執行App的Assembly
            //Assembly assembly = Assembly.GetExecutingAssembly();

            // 取得Service的Assembly
            string serviceName = "Health.Service";
            Assembly assemblyService = Assembly.Load(serviceName);

            // 取得Repository的Assembly
            string repositoryName = "Health.Repository";
            Assembly assemblyRepository = Assembly.Load(repositoryName);

            //if (assemblyService == null)
            //{
            //    assemblyService = BuildManager.GetReferencedAssemblies().Cast<Assembly>()
            //        .Where(x => x.FullName.Contains(serviceName)).FirstOrDefault();
            //}

            // B.註冊所有名稱為Service結尾的物件
            builder.RegisterAssemblyTypes(assemblyService)
                .Where(x => x.Name.EndsWith("Service", StringComparison.Ordinal))
                .AsImplementedInterfaces();

            // 註冊所有名稱為Repository結尾的物件
            builder.RegisterAssemblyTypes(assemblyRepository)
                .Where(x => x.Name.EndsWith("Repository", StringComparison.Ordinal))
                .AsImplementedInterfaces();

            // === 3. 由Builder建立容器 ===
            container = builder.Build();
        }
    }
}
