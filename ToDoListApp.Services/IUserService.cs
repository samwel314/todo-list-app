using Microsoft.AspNetCore.Identity;
using ToDoListApp.Models;
using ToDoListApp.Services.Dto;

namespace ToDoListApp.Services
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterNewUser(UserRegisterDto model);
        Task<AuthenticationResponseDto> ValidateUser(UserLoginDto model);
        Task<string> CreateToken(User user);
    }
}
