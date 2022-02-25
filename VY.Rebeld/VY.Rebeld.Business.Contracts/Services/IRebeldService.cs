using System.Threading.Tasks;
using VY.Rebeld.Dtos;
using VY.Rebeld.Infrastructure.Contracts;

namespace VY.Rebeld.Business.Contracts.Services
{
    public interface IRebeldService
    {
        Task<OperationResult<RebeldSightingDto>> GetRebeldSightingAsync(string name);
        Task<OperationResult> SaveRebeldAsync(RebeldDto rebeldDto);
    }
}