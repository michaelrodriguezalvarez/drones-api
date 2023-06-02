using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Drones.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Drones.Drones.Dto
{
    [AutoMapFrom(typeof(Drone))]
    [AutoMapTo(typeof(Drone))]
    public class DroneDto : EntityDto<long>
    {
        public int? TenantId { get; set; }
        [MaxLength(100)]
        public string SerialNumber { get; set; }
        public string Model { get; set; }
        [Range(0, 500)]
        public decimal Weight { get; set; }
        [Range(0, 100)]
        public int BatteryCapacity { get; set; }
        public string State { get; set; }
    }
}
