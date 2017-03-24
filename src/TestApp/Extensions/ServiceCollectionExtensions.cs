using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using TestApp.Business.Managers;

namespace TestApp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            Assembly dataAssembly = Assembly.Load(new AssemblyName("TestApp.Data"));

            services.Scan(scan => scan.FromAssemblies(dataAssembly)
                .AddClasses(classes => classes.Where(c => c.Name.Equals("DataAccess")))
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime());
            services.Scan(scan => scan.FromAssemblies(dataAssembly)
                .AddClasses(classes => classes.Where(c => c.Name.EndsWith("Repository")))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                );

            services.Scan(scan => scan.FromAssemblyOf<UsersManager>()
                .AddClasses(classes => classes.Where(c => c.Name.EndsWith("Manager")))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            //services. AddSingleton<IDataAccess, DataAccess>();

            return services;
        }
    }
}
