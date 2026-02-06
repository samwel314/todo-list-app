using System.ComponentModel.DataAnnotations;

namespace ToDoListApp.Services.Dto
{
    public class  UserLoginDto 
    {
        [Required(ErrorMessage = "User Email is required")]
        [EmailAddress]
        public string? Email { get; init; }
        [Required(ErrorMessage = "Password name is required")]
        public string? Password { get; init; }
    }
}
