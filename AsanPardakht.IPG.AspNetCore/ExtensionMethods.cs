using AsanPardakht.IPG;
using AsanPardakht.IPG.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ExtensionMethods
    {
        public static IServiceCollection AddAsanPardakhtIpg(this IServiceCollection services)
        {
            services.AddSingleton<IClient, Client>();
            services.AddSingleton<IServices, Services>();

            return services;
        }
        public static IServiceCollection AddAsanPardakhtIpg(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<Config>(configuration.GetSection("AsanPardakhtIPGConfig"));
            services.AddSingleton<Config>(s => configuration.GetSection("AsanPardakhtIPGConfig").Get<Config>());

            services.AddAsanPardakhtIpg();

            return services;
        }
    }
}
