using Abp.Authorization;
using Drones.Authorization.Roles;
using Drones.Authorization.Users;

namespace Drones.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
