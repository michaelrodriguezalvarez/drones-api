using System.Threading.Tasks;
using Drones.Configuration.Dto;

namespace Drones.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
