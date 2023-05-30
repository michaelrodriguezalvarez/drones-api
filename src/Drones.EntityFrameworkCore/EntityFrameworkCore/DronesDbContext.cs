using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Drones.Authorization.Roles;
using Drones.Authorization.Users;
using Drones.MultiTenancy;

namespace Drones.EntityFrameworkCore
{
    public class DronesDbContext : AbpZeroDbContext<Tenant, Role, User, DronesDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public DronesDbContext(DbContextOptions<DronesDbContext> options)
            : base(options)
        {
        }
    }
}
