using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Drones.Configuration.Dto;

namespace Drones.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : DronesAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
