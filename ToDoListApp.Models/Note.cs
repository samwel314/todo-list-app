using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoListApp.Models
{
    public class Note
    {
        public int NoteId { get; set; }
        [MaxLength (200)]
        public string Progress_Note { get; set; } = null!; 
        public DateTime CreatedOrUpdatedAt { get; set; }   = DateTime.Now;     
        public int TaskId { get; set; } 
        [ForeignKey ("TaskId")]
        public TaskToDO Task { get; set; } = null!; 
    }

}
