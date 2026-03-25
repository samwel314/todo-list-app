using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoListApp.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string TagName { get; set; } = null!;
        public string UserId { get; set; } = null!;
        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
    }
}
