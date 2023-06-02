using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Drones.Models;

namespace Drones.Drones.Dto
{
    [AutoMapFrom(typeof(DroneMedication))]
    [AutoMapTo(typeof(DroneMedication))]
    public class DroneMedicationDto : EntityDto<long>
    {
        public int? TenantId { get; set; }
        public long DroneId { get; set; }
        public long MedicationId { get; set; }
    }
}
