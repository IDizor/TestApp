using System.Reflection;

using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using TestApp.Business.Managers;

namespace TestApp.Extensions
{
    /// <summary>
    /// Extends ServiceCollection class with RegisterDependencies method.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the dependencies for application.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            Assembly dataAssembly = Assembly.Load(new AssemblyName("TestApp.Data"));

            // Scrutor is used to registed unreferenced assembly dependencies
            services.Scan(scan => scan.FromAssemblies(dataAssembly)
                .AddClasses(classes => classes.Where(c => c.Name.Equals("DataAccess")))
                .AsImplementedInterfaces()
                .WithSingletonLifetime());

            // register dependencies by class name pattern
            services.Scan(scan => scan.FromAssemblies(dataAssembly)
                .AddClasses(classes => classes.Where(c => c.Name.EndsWith("Repository")))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.Scan(scan => scan.FromAssemblyOf<UsersManager>()
                .AddClasses(classes => classes.Where(c => c.Name.EndsWith("Manager")))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }
    }
}
