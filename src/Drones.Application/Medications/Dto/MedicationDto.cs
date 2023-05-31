using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Drones.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Drones.Medications.Dto
{
    [AutoMapFrom(typeof(Medication))]
    [AutoMapTo(typeof(Medication))]
    public class MedicationDto : EntityDto<long>
    {
        public int? TenantId { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9\-_]+$")]
        public string Name { get; set; }
        public decimal Weight { get; set; }
        [RegularExpression(@"^[A-Z0-9_]+$")]
        public string Code { get; set; }
        public string Image { get; set; }
    }
}
