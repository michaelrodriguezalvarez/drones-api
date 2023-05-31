using Abp.Application.Services;
using Drones.Drones.Dto;

namespace Drones.Drones
{
    public interface IDroneAppService : IAsyncCrudAppService<DroneDto, long, PagedDronesResultRequestDto, DroneDto>
    {
    }
}
