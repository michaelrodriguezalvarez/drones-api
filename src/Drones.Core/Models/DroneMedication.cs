using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Drones.Models
{
    public class DroneMedication : FullAuditedEntity<long>, IMayHaveTenant
    {
        public int? TenantId { get; set; }
        public long DroneId { get; set; }
        public long MedicationId { get; set; }
        public virtual Drone Drone { get; set; }
        public virtual Medication Medication { get; set; }
    }
}
