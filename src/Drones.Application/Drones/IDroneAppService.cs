using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Drones.Drones.Dto;
using Drones.Medications.Dto;
using System.Threading.Tasks;

namespace Drones.Drones
{
    public interface IDroneAppService : IAsyncCrudAppService<DroneDto, long, PagedDronesResultRequestDto, DroneDto>
    {
        Task<bool> Register(DroneDto input);
        Task<bool> Load(LoadDronesRequestDto input);
        Task<PagedResultDto<MedicationDto>> CheckLoaded(CheckDronesRequestDto input);
        Task<PagedResultDto<DroneDto>> CheckAvailables(CheckAvailablesRequestDto input);
        Task<int> CheckBatteryLevel(CheckDronesRequestDto input);
        Task<bool> Unload(UnloadDroneDto input);
        decimal CheckLoadedWeight(CheckLoadedWeightDroneDto input);
    }
}
