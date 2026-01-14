using System.ComponentModel.DataAnnotations;

namespace ToDoListApp.Services.Dto
{
    public class CreateUpdateTagDto
    {
        [Required]
        [MinLength (1)]
        [MaxLength (30)]
        public string TagName { get; set; } = null!;
        // fetch from api call token claims
        public string ? UserId { get; set; } = null!; 
    }
}
