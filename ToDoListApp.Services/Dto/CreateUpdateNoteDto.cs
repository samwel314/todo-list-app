using System.ComponentModel.DataAnnotations;

namespace ToDoListApp.Services.Dto
{
    public class CreateUpdateNoteDto
    {
        [MinLength(5)]
        [MaxLength(100)]
        [Required]
        public string Progress_Note { get; set; } = null!;
        public int TaskId { get; set; }
    }   
}
