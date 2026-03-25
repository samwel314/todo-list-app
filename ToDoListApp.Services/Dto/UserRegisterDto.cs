using System.ComponentModel.DataAnnotations;

namespace ToDoListApp.Services.Dto
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "Please Enter Your First Name ")]
        public string? FirstName { get; init; }
        [Required(ErrorMessage = "Please Enter Your Last Name ")]
        public string? LastName { get; init; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; init; } = null;
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; init; }
    }
}
