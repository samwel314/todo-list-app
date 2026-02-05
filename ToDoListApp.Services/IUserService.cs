using Microsoft.AspNetCore.Identity;
using ToDoListApp.Services.Dto;

namespace ToDoListApp.Services
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterNewUser(UserRegisterDto model); 
    }
}
