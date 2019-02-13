using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Core.Common.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Api.Extensions
{
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// Patterns of dll/s where are located all classes that should be binded with interfaces
        /// </summary>
        private static readonly string[] _classesPatterns = { "Services", "Repository" };
        /// <summary>
        /// Pattern of dll/s where are all interfaces
        /// </summary>
        private static readonly string _interfacesPattern = "Domain";

        /// <summary>
        /// Binds all interfaces wich implement T
        /// </summary>
        /// <param name="appDomain">AppDomain from the assembly we are binding interfaces</param>
        /// <param name="lifetime">Lifetime of the interfaces wich method will bind</param>
        /// <typeparam name="T">Interface wich is implemented by others (we want to bind T "sons")</typeparam>
        public static void BindAll<T>(this IServiceCollection services, AppDomain appDomain, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            LoadUnusedAssemblies(appDomain);
            Assembly[] assemblies = appDomain.GetAssemblies();
            var types = assemblies.Where(x => x.FullName.Contains(_interfacesPattern))
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(T).IsAssignableFrom(p));

            foreach (var type in types)
            {
                if (type.IsInterface)
                {
                    var classes = assemblies.Where(x => x.FullName.Contains(_classesPatterns))
                        .SelectMany(s => s.GetTypes())
                        .Where(p => type.IsAssignableFrom(p))
                        .ToList();
                    foreach (var c in classes)
                        services.Add(new ServiceDescriptor(type, c, lifetime));
                    continue;
                }
                // If type is not an interface should be injected
                services.Add(new ServiceDescriptor(typeof(T), type, lifetime));
            }
        }

        private static void LoadUnusedAssemblies(AppDomain appDomain)
        {
            try
            {
                var loadedAssemblies = appDomain.GetAssemblies().ToList();
                var loadedPaths = loadedAssemblies.Select(a => a.Location).ToArray();

                var referencedPaths = Directory.GetFiles(appDomain.BaseDirectory, "*.dll").Where(x => x.Contains(_classesPatterns));
                var toLoad = referencedPaths.Where(r => !loadedPaths.Contains(r, StringComparer.InvariantCultureIgnoreCase)).ToList();
                toLoad.ForEach(path => loadedAssemblies.Add(appDomain.Load(AssemblyName.GetAssemblyName(path))));
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine($"Couldn't access the {appDomain.FriendlyName} base directory to get unused assemblies to load them");
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Binds all interfaces wich implement T<W>.
        /// If T not generic <see cref="BindAll{T}"/>.
        /// </summary>
        /// <param name="appDomain">AppDomain from the assembly we are binding interfaces</param>
        /// <param name="lifetime">Lifetime of the interfaces wich method will bind</param>
        /// <param name="t">Interface wich is implemented by others (we want to bind T<W> "sons")</param>
        public static void BindAllFromGeneric(this IServiceCollection services, AppDomain appDomain, Type t, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            LoadUnusedAssemblies(appDomain);
            Assembly[] assemblies = appDomain.GetAssemblies();
            var classes = assemblies.Where(x => x.FullName.Contains(_classesPatterns));
            foreach (var assembly in classes)
            {
                foreach (var type in assembly.GetTypes()
                    .Where(ty => ty.IsClass && !ty.IsAbstract))
                {
                    foreach (var i in type.GetInterfaces())
                    {
                        if (i.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == t))
                        {
                            services.Add(new ServiceDescriptor(i, type, lifetime));
                        }
                    }
                }
            }
        }
    }
}
