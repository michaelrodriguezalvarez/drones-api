using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Drones.Controllers
{
    public abstract class DronesControllerBase: AbpController
    {
        protected DronesControllerBase()
        {
            LocalizationSourceName = DronesConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
