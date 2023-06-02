using Abp.Application.Services;
using Drones.Medications.Dto;

namespace Drones.Medications
{
    public interface IMedicationAppService : IAsyncCrudAppService<MedicationDto, long, PagedMedicationsResultRequestDto, MedicationDto>
    {
    }
}
