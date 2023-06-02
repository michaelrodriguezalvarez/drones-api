using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Drones.Configuration;
using Abp.Hangfire;
using Abp.Hangfire.Configuration;

namespace Drones.Web.Host.Startup
{
    [DependsOn(
       typeof(DronesWebCoreModule),
         typeof(AbpHangfireAspNetCoreModule))]
    public class DronesWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public DronesWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.BackgroundJobs.UseHangfire();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DronesWebHostModule).GetAssembly());
        }
    }
}
