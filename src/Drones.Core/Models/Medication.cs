using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Drones.Models
{
    public class Medication : FullAuditedEntity<long>, IMayHaveTenant
    {
        public Medication()
        {
            DronesMedications = new HashSet<DroneMedication>();
        }
        public int? TenantId { get; set; }
        public string Name { get; set; }
        public decimal Weight { get; set; }
        public string Code { get; set; }
        public string Image { get; set; }

        public virtual ICollection<DroneMedication> DronesMedications { get; set; }
    }
}
