using System.ComponentModel.DataAnnotations;

namespace ToDoListApp.Services.Dto
{
    public class UserRegisterDto
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        [Required(ErrorMessage = "Username is required")]
        public string? UserName { get; init; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; init; }
        public string? PhoneNumber { get; init; }
    }
}
