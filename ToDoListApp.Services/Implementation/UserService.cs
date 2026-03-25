using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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

        public async Task<AuthenticationResponseDto> ValidateUser(UserLoginDto model)
        {
            AuthenticationResponseDto response = new AuthenticationResponseDto();
             var user = await  _userManager.FindByEmailAsync(model.Email!);
            if (user is not null)
            {
                response.IsAuthenticated = await _userManager.CheckPasswordAsync(user, model.Password!); 
            }

            if (!response.IsAuthenticated)
            {
                response.Message = "Email or Password Not Valid" ;
                return response;
            }
            // generate token   

            response.Token = await CreateToken(user!);
            response.Message = $"Hi {user!.FirstName}";
            return response;
        }

        public async Task<string> CreateToken(User user)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
        private SigningCredentials GetSigningCredentials()
        {
            // بيعمل ايه ؟ ( signing credentials بيرجع ال اللي هتستخدم في توليد التوكن
            var key = Encoding.UTF8.GetBytes(
                _configuration["JwtSettings:Key"]!);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        // هنا ال app مفهوش اي ادوار بس لازم اميز كل token 
        private async Task<List<Claim>> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
               new Claim(ClaimTypes.NameIdentifier, user!.Id),
               new Claim(ClaimTypes.Name, user.UserName!)
            };
            return claims; 
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials,List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            Console.WriteLine(DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])));
            var tokenOptions = new JwtSecurityToken
            (
            issuer: jwtSettings["validIssuer"],
            audience: jwtSettings["ValidAudiences"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
            signingCredentials: signingCredentials
            );

            return tokenOptions;
        }
    }

}
