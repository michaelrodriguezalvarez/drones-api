using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Drones.EntityFrameworkCore;
using Drones.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Drones.Web.Tests
{
    [DependsOn(
        typeof(DronesWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class DronesWebTestModule : AbpModule
    {
        public DronesWebTestModule(DronesEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DronesWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(DronesWebMvcModule).Assembly);
        }
    }
}