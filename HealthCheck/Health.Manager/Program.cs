using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Autofac;
using AutoMapper;
using Health.Repository.Dto;
using Health.Service.Interfaces;

namespace Health.Manager
{
    class Program
    {
        static IContainer container;
        static IMapper mapper;

        static void Main(string[] args)
        {
            RegisterAutofacConfig();
            RegisterAutoMapper();

            IHealthService service = container.Resolve<IHealthService>();
            IEnumerable<HealthTargetDto> targets = service.GetActiveTarget();

            if ((args.Count() > 0) && string.Compare(args[0], "async", true) == 0)
            {
                Task.Run(async () =>
                {
                    foreach (HealthTargetDto target in targets)
                    {
                        CheckerRunner runner = new CheckerRunner(target, mapper, service);
                        await runner.RunAsync();
                    }
                }).Wait();
            }
            else
            {
                Parallel.ForEach(targets, (target) =>
                {
                    CheckerRunner runner = new CheckerRunner(target, mapper, service);
                    runner.Run();
                });
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

        static void RegisterAutoMapper()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HealthTargetDto, HealthCurrentDto>();
                cfg.CreateMap<HealthTargetDto, HealthHisDto>();
            });
            mapper = mapperConfiguration.CreateMapper();
        }
    }
}
