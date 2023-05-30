using System.Threading.Tasks;
using Abp.Application.Services;
using Drones.Sessions.Dto;

namespace Drones.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
