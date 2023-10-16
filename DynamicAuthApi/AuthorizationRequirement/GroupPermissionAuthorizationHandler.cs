using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DynamicAuthApi.AuthorizationRequirement
{
    public class GroupPermissionAuthorizationHandler : AuthorizationHandler<GroupPermissionRequirement>
    {
        private readonly Context _context;
        public GroupPermissionAuthorizationHandler(Context context)
        {
            _context = context;
        }

        protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        GroupPermissionRequirement requirement)
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userId != null)
            {
                var PermissionId = _context.Permissions.FirstOrDefault(p=>p.Name == requirement.PermissionName).Id;
                if(PermissionId > 0)
                {
                    var permission = _context.Permissions.Find(PermissionId);
                var userGroup = _context.UserGroups.Where(g=> g.UserId == userId).ToList();
                    var groupPermission = _context.GroupPermissions.Where(g => g.PermissionId == PermissionId).ToList();
                    foreach(var group in groupPermission)
                    {
                        var UserHasPermission = userGroup.Any(g=>g.GroupId == group.GroupId);
                        if(UserHasPermission)
                        {
                            context.Succeed(requirement);

                        }
                    }


                }
            }
            //g.GroupId == groupId.Where(g=>g.GroupId )
            //.Include("permission")
            // Check if the user belongs to a group with the required permission
            // You'll need to implement this logic using your DbContext


            return Task.CompletedTask;
        }
    }
}
