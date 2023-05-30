using System.Threading.Tasks;
using Drones.Models.TokenAuth;
using Drones.Web.Controllers;
using Shouldly;
using Xunit;

namespace Drones.Web.Tests.Controllers
{
    public class HomeController_Tests: DronesWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}