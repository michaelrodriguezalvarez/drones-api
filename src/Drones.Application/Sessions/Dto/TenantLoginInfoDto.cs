using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Drones.MultiTenancy;

namespace Drones.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}
