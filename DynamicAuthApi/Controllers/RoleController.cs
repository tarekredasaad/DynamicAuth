using Domain.DTO;
using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace DynamicAuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService roleService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public RoleController(IRoleService roleService
            , RoleManager<IdentityRole> RoleManager
            , UserManager<ApplicationUser> userManager
)
        {
            this.roleService = roleService;
            roleManager = RoleManager;
            this.userManager = userManager;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("addRole")]
        public async Task<ActionResult<ResultDTO>> AddRole(RoleDTO roleDTO)
        {
            if (User.Identity.IsAuthenticated)
            {

                if (!ModelState.IsValid) { return BadRequest(new ResultDTO() { StatusCode = 400, Data = ModelState }); };
                IdentityRole roleModel = new IdentityRole();
                roleModel.Name = roleDTO.RoleName;
                IdentityResult result = await roleManager.CreateAsync(roleModel);//unique
                if (result.Succeeded)
                {
                    return Ok(new ResultDTO()
                    {
                        StatusCode = 200,
                        Data = "Role Is Added successfully"
                    });
                }
                else
                {
                    return BadRequest(new ResultDTO() { StatusCode = 400, Data = "Invalid operation" });
                }
            }
            else
            {
                return Forbid("UnAuthorized");

            }
            //return Ok(roleService.AddRole(roleDTO));
        }
        [Authorize]
        [HttpPost("addPermission")]
        public async Task<ActionResult<ResultDTO>> AddPermission(string roleName, string permission)
        {
            if (!string.IsNullOrEmpty(roleName) && !string.IsNullOrEmpty(permission))
            {
                var role = await roleManager.FindByNameAsync(roleName);
                if (role != null)
                {
                    var claim = new Claim("Permission", permission);
                    await roleManager.AddClaimAsync(role, claim);
                    return Ok(new ResultDTO()
                    {
                        StatusCode = 200,
                        Data = $"Permission {permission} added to role {roleName}."
                    } );
                }
                return BadRequest(new ResultDTO() { StatusCode = 400, Data = ModelState });
            }
            return BadRequest(new ResultDTO() { StatusCode = 400, Data = ModelState });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("addRoleToUser")]
        public async Task<ActionResult<ResultDTO>> AddRoleToUser(string roleName, string email)
        {
            if (!string.IsNullOrEmpty(roleName) && !string.IsNullOrEmpty(email))
            {
                var user = await userManager.FindByEmailAsync(email); // Replace with the username of the user.

                if (user != null)
                {
                    var result = await userManager.AddToRoleAsync(user, roleName); // Replace with the role name you want to assign.

                    if (result.Succeeded)
                    {
                        return Ok(new ResultDTO()
                        {
                            StatusCode = 200,
                            Data = $"ROle added to User successfully."
                        });
                    }
                    else
                    {
                        return BadRequest(new ResultDTO() { StatusCode = 400, Data = ModelState });
                    }
                }
            }
            return BadRequest(new ResultDTO() { StatusCode = 400, Data = ModelState });

        }
    }
}
