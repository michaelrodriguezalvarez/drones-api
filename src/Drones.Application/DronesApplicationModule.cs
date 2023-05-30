using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Drones.Authorization;

namespace Drones
{
    [DependsOn(
        typeof(DronesCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class DronesApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<DronesAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(DronesApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
