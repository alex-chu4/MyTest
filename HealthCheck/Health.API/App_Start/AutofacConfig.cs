using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Http;

using Autofac;
using Autofac.Integration.WebApi;

namespace Health.API
{
    public class AutofacConfig
    {
        public static void Run()
        {
            // === 1. 建立容器 ===
            var builder = new ContainerBuilder();

            // === 2. 註冊服務 ===
            // 取得目前執行App的Assembly
            Assembly assembly = Assembly.GetExecutingAssembly();

            // 取得Service的Assembly
            string serviceName = "Health.Service";
            Assembly assemblyService = Assembly.Load(serviceName);

            // 取得Repository的Assembly
            string repositoryName = "Health.Repository";
            Assembly assemblyRepository = Assembly.Load(repositoryName);

            if (assemblyService == null)
            {
                assemblyService = BuildManager.GetReferencedAssemblies().Cast<Assembly>()
                    .Where(x => x.FullName.Contains(serviceName)).FirstOrDefault();
            }

            // B.註冊所有名稱為Service結尾的物件
            builder.RegisterAssemblyTypes(assemblyService)
                .Where(x => x.Name.EndsWith("Service", StringComparison.Ordinal))
                .AsImplementedInterfaces();

            // 註冊所有名稱為Repository結尾的物件
            builder.RegisterAssemblyTypes(assemblyRepository)
                .Where(x => x.Name.EndsWith("Repository", StringComparison.Ordinal))
                .AsImplementedInterfaces();

            // ***重要*** 註冊Controller和ApiController
            builder.RegisterApiControllers(assembly);

            // === 3. 由Builder建立容器 ===
            var container = builder.Build();

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // === 4. 把容器設定給DependencyResolver ===
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}