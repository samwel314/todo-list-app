using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
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

        public async Task<IdentityResult> RegisterNewUser(UserRegisterDto model)
        {
           var user = new User
            {
                FirstName = model.FirstName!,
                LastName = model.LastName!,
                Email = model.Email!,
                UserName = model.Email!
           };
            
            var result = await _userManager.CreateAsync(user, model.Password!);
            return result;
        }
    }
   
}
