using Domain.Interfaces.Services;
using Domain.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.UnitOfWork;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Domain.DTO;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork unitOfWork;
        public bool Authenticate_User { get; private set; }
        public AuthService(
             SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork ) 
        {
            Authenticate_User = false;
            this.unitOfWork = unitOfWork;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public  ResultDTO GetAllUsers()
        {
            List<ApplicationUser> applicationUsers;
            applicationUsers =  _userManager.Users.ToList();
            return new ResultDTO() { StatusCode = 400, Data = applicationUsers };
        }
        public async Task<ResultDTO> Login(UserDTO userDTO)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(userDTO.Email);
            if (user == null) { return new ResultDTO() { StatusCode = 400, Data = "User Not found" }; }

            var result = await _signInManager.CheckPasswordSignInAsync(user, userDTO.Password, false);
            if (!result.Succeeded) {

                return new ResultDTO() { StatusCode = 400, Data = "Invalid Data" };
            }
            else
            {
                await _signInManager.SignInAsync(user, isPersistent: false);

                return new ResultDTO()
                {
                    StatusCode = 200,
                    Data = "Authentication successful.",
                };
            }



        }
        public async Task<ResultDTO> Register(UserDTO userDTO) 
        {
            ResultDTO resultDTO = new ResultDTO();
            if (userDTO != null)
            {
                ApplicationUser applicationUser = new ApplicationUser();
                applicationUser.Email = userDTO.Email;
                Random rnd = new Random();
                applicationUser.UserName = $"{"User"}{rnd.Next(1, 1000)}";
                IdentityResult result = await _userManager.CreateAsync(applicationUser, userDTO.Password);
                if (result.Succeeded)
                {
                    resultDTO.StatusCode = 200;
                    // await userManager.AddToRoleAsync(applicationUser, "User");//insert row UserRole
                    return resultDTO;
                }
                else
                {
                    resultDTO.StatusCode = 404;
                   
                    resultDTO.Data = "null or bad request";
                    return resultDTO;
                }

            }
            resultDTO.StatusCode = 404;
            resultDTO.Data = "null or bad request";
            return resultDTO;
        }

        public async Task<ResultDTO> Logout( )
        {
            ResultDTO resultDTO = new ResultDTO();
              await _signInManager.SignOutAsync();
            return new ResultDTO(){
                StatusCode = 200,
                Data="Signout Is successed"
            };
        } 
        public async Task<ResultDTO> DeleteUser(string emial )
        {
            if (!string.IsNullOrEmpty(emial))
            {
              var user = await   _userManager.FindByEmailAsync(emial);
                if (user != null)
                {
                    await _userManager.DeleteAsync(user);
                    return new ResultDTO()
                    {
                        StatusCode = 200,
                        Data = "User Is deleted successfully"
                    };
                }
                return new ResultDTO()
                {
                    StatusCode = 400,
                    Data = "null or bad request"
                };
            }
                
            return new ResultDTO(){
                StatusCode = 400,
                Data= "null or bad request"
            };
        }
        public bool CheckAuthenticateUser(string username, string password)
        {
            //if(unitOfWork.UserRepository.Get(x=> x.UserName == username && x.Password == password) != null)
            //{
            //    Authenticate_User = true;
            return Authenticate_User;

            //}else 
            //{
            //    Authenticate_User = false;
            //    return Authenticate_User; 
            //}
        }
        public void SignOut()
        {
            Authenticate_User = false;            
        }
    }
}
