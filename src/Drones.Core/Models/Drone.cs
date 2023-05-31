using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Drones.Models
{
    public class Drone : FullAuditedEntity<long>, IMayHaveTenant
    {
        public Drone()
        {
            DronesMedications = new HashSet<DroneMedication>();
        }
        public int? TenantId { get; set; }
        [MaxLength(100)]
        public string SerialNumber { get; set; }
        public string Model { get; set; }
        [Range(0, 500)]
        public decimal Weight { get; set; }
        [Range(0, 100)]
        public int BatteryCapacity { get; set; }
        public string State { get; set; }

        public virtual ICollection<DroneMedication> DronesMedications { get; set; }
    }

    public enum DronesModels
    {
        Lightweight, 
        Middleweight, 
        Cruiserweight, 
        Heavyweight
    }
    public enum DronesStates
    {
        IDLE, 
        LOADING, 
        LOADED, 
        DELIVERING, 
        DELIVERED, 
        RETURNING
    }
}
