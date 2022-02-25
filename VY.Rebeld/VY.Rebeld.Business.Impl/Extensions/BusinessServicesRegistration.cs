using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VY.Rebeld.Business.Impl.MappingProfiles;
using VY.Rebeld.Data.Impl.Extensions;
using VY.Rebeld.Business.Contracts.Services;
using VY.Rebeld.Business.Impl.Services;

namespace VY.Rebeld.Business.Impl.Extensions
{
    public static class BusinessServicesRegistration
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection service, IConfiguration config)
        {
            service.AddDataServices(config);

            service.AddTransient<IRebeldService, RebeldService>();

            service.AddAutoMapper(typeof(RebeldProfile));

            return service;
        }
    }
}
