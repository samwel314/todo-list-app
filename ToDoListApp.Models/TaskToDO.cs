using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoListApp.Models
{
    public class TaskToDO
    {
        [Key] // or use fluent api to make it pk 
        public int TaskId { get; set; }
        // gave it len 150
        [MaxLength(150)]
        public string Title { get; set; } = null!; 
        // max len 
        public string Description { get; set; } = null!; 

        // Not Started - In Progress - Completed - Time Out
        public TaskStatus Status { get; set; } = TaskStatus.NotStarted;
        // the time user create it 
        public DateTime CreatedAT { get; set; }  = DateTime.Now;   
        // make it completed 
        public bool IsCompleted { get; set; }
        // 
        public DateTime ? CompletedAT { get; set; }
        // if the day is left and user not make it become time out
        public DateTime ExpectedEndDate { get; set; }
        public int TagId { get; set; }
        [ForeignKey("TagId")]
        public Tag Tag { get; set; } = null!;       
    }
}
