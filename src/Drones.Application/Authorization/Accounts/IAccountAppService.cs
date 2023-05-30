using System.Threading.Tasks;
using Abp.Application.Services;
using Drones.Authorization.Accounts.Dto;

namespace Drones.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
