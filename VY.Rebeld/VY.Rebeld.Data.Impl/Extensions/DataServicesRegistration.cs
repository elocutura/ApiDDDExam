using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VY.Rebeld.Data.Contracts.Repositories;
using VY.Rebeld.Data.Impl.Repositories;

namespace VY.Rebeld.Data.Impl.Extensions
{
    public static class DataServicesRegistration
    {
        public static IServiceCollection AddDataServices(this IServiceCollection service, IConfiguration config)
        {

            service.AddTransient<IRebeldRepository>(c => new FileRebeldRepository(config["FilePath"]));

            return service;
        }
    }
}
