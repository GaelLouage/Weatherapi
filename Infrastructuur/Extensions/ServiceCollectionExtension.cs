using Infrastructuur.Data.Classes;
using Infrastructuur.Data.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructuur.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureServicesExtensions(this IServiceCollection services) =>
                 services
                .AddScoped<IWeaterData, WeatherData>()
                .AddSingleton<IMemoryCache, MemoryCache>();
    }
}
