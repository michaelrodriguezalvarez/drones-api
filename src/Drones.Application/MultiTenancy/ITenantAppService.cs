using Abp.Application.Services;
using Drones.MultiTenancy.Dto;

namespace Drones.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

