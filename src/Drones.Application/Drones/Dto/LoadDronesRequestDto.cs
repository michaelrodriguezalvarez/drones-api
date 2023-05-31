using Drones.Medications.Dto;
using System.Collections.Generic;

namespace Drones.Drones.Dto
{
    public class LoadDronesRequestDto
    {
        public long DroneId { get; set; }
        public List<long> MedicationItems { get; set; }
    }
}
