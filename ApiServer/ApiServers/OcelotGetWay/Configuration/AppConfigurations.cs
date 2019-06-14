using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace OcelotGetWay.Configuration
{
    public static class AppConfigurations
    {
        private static readonly ConcurrentDictionary<string, IConfigurationRoot> _configurationCache;

        static AppConfigurations()
        {
            _configurationCache = new ConcurrentDictionary<string, IConfigurationRoot>();
        }

        public static IConfigurationRoot Get(string path, string environmentName = null, bool addUserSecrets = false)
        {
            var cacheKey = path + "#" + environmentName + "#" + addUserSecrets;
            return _configurationCache.GetOrAdd(
                cacheKey,
                _ => BuildConfiguration(path, environmentName, addUserSecrets)
            );
        }

        private static IConfigurationRoot BuildConfiguration(string path, string environmentName = null, bool addUserSecrets = false)
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("OcelotConfig.json", optional: true, reloadOnChange: true);



            if (!string.IsNullOrEmpty(environmentName))
            {
                builder = builder.AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true);
            }

            builder = builder.AddEnvironmentVariables();

            if (addUserSecrets)
            {
                builder.AddUserSecrets(typeof(AppConfigurations).GetAssembly());
            }

            return builder.Build();
        }
    }
    public static class TypeExtensions
    {
        public static Assembly GetAssembly(this Type type)
        {
            return type.GetTypeInfo().Assembly;
        }

        public static MethodInfo GetMethod(this Type type, string methodName, int pParametersCount = 0, int pGenericArgumentsCount = 0)
        {
            return type
                .GetMethods()
                .Where(m => m.Name == methodName).ToList()
                .Select(m => new
                {
                    Method = m,
                    Params = m.GetParameters(),
                    Args = m.GetGenericArguments()
                })
                .Where(x => x.Params.Length == pParametersCount
                            && x.Args.Length == pGenericArgumentsCount
                ).Select(x => x.Method)
                .First();
        }
    }
}
