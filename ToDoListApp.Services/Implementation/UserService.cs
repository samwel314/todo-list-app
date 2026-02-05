using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ToDoListApp.Data.Repository;
using ToDoListApp.Models;
using ToDoListApp.Services.Dto;

namespace ToDoListApp.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public IdentityResult RegisterNewUser(UserRegisterDto model)
        {
           var user = new User
            {
                FirstName = model.FirstName!,
                LastName = model.LastName!,
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber
            };
            
            var result = _userManager.CreateAsync(user, model.Password!).Result;
            return result;
        }
    }
   
}
