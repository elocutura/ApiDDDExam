using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VY.Rebeld.Data.Contracts.Entities;
using VY.Rebeld.Dtos;
using VY.Rebeld.Infrastructure.Contracts;

namespace VY.Rebeld.Data.Contracts.Repositories
{
    public interface IRebeldRepository
    {

        public Task<OperationResult<RebeldSightingDto>> GetSightingByNameAsync(string name);
        public Task<OperationResult> SaveRebeldAsync(RebeldEntity rebeldEntity);
    }
}
