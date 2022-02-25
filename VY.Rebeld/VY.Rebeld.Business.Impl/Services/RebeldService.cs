using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VY.Rebeld.Business.Contracts.Domain;
using VY.Rebeld.Business.Contracts.Services;
using VY.Rebeld.Data.Contracts.Entities;
using VY.Rebeld.Data.Contracts.Repositories;
using VY.Rebeld.Dtos;
using VY.Rebeld.Infrastructure.Contracts;

namespace VY.Rebeld.Business.Impl.Services
{
    public class RebeldService : IRebeldService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly IRebeldRepository _rebeldRepo;

        public RebeldService(ILogger<RebeldService> logger, IMapper mapper, IMemoryCache cache, IRebeldRepository rebeldRepo)
        {
            _logger = logger;
            _mapper = mapper;
            _cache = cache;
            _rebeldRepo = rebeldRepo;
        }

        public async Task<OperationResult> SaveRebeldAsync(RebeldDto rebeldDto)
        {
            var toReturn = new OperationResult();

            // this rebeld domainEntity is not needed in this example, but if we needed to manage this data in business its good practice
            // to have DomainEntities.
            var rebeldDomain = _mapper.Map<RebeldDomain>(rebeldDto);
            var rebeldEntity = _mapper.Map<RebeldEntity>(rebeldDomain);

            var result = await _rebeldRepo.SaveRebeldAsync(rebeldEntity);

            // No need to check if result has errors, if there is no errors, none will be added
            toReturn.AddError(result.GetAllErrors());

            // Error Logging
            if (result.HasErrors())
            {
                foreach (var error in result.GetAllErrors())
                {
                    _logger.LogError(error.Message);
                }
            }

            return toReturn;
        }

        public async Task<OperationResult<RebeldSightingDto>> GetRebeldSightingAsync(string name)
        {
            var toReturn = new OperationResult<RebeldSightingDto>();

            // Check input
            if (name == null || name == "")
            {
                toReturn.AddError(400, "Name is null or empty");
                return toReturn;
            }

            // Try and find that rebeld in cache, if we find it return the cached value
            RebeldSightingDto cachedValue;
            if (_cache.TryGetValue(name, out cachedValue))
            {
                toReturn.Result = cachedValue;
            }
            else
            { 
                var result = await _rebeldRepo.GetSightingByNameAsync(name);

                // Cache in memory the results and expiration of 1 minute, where the will need to be cached again
                _cache.Set(name, result.Result, DateTimeOffset.Now.AddMinutes(1));

                // No need to check if result has errors, if there is no errors, none will be added
                toReturn.AddError(result.GetAllErrors());

                // Error Logging
                if (result.HasErrors())
                {
                    foreach (var error in result.GetAllErrors())
                    {
                        _logger.LogError(error.Message);
                    }
                }

                toReturn.Result = result.Result;
            }

            return toReturn;
        }

    }
}
