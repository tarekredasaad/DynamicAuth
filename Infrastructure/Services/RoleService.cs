using Domain.DTO;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
//using System.Web.Http.Results;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace Infrastructure.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly UserManager<IdentityUser> _userManager;

        public RoleService(RoleManager<IdentityRole> roleManager
            ,IServiceProvider serviceProvider
            //, UserManager<IdentityUser> userManager  
            ) 
        {
            //_roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            _roleManager = roleManager;
            //_userManager = userManager;
        }
        public async Task<ResultDTO> AddRole(RoleDTO roleDTO)
        {
            IdentityRole roleModel = new IdentityRole();
            roleModel.Name = roleDTO.RoleName;
           
            IdentityResult result = await _roleManager.CreateAsync(roleModel);
            //var jsonResult = new JsonResult(result);
            if (result.Succeeded)
            {
                return  new ResultDTO()
                {
                    StatusCode = 200,
                    Data = "Role Is Added successfully"
                };
            }
            else
            {
                return new ResultDTO() { StatusCode = 400, Data = "Invalid operation" };
            }
        }
    }
}
