using Microsoft.AspNetCore.Authorization;

namespace DynamicAuthApi.AuthorizationRequirement
{
    public class GroupPermissionRequirement : IAuthorizationRequirement
    {
        public string PermissionName { get; }

        public GroupPermissionRequirement(string permissionName)
        {
            PermissionName = permissionName;
        }
    }
}
