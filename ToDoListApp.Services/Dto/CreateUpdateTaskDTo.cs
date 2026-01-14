using System.ComponentModel.DataAnnotations;

namespace ToDoListApp.Services.Dto
{
    public class CreateUpdateTaskDTo
    {
        // we will add user id 
        [MaxLength(70)]
        [MinLength(5)]
        public string Title { get; set; } = null!;
        [MinLength(5)]
        public string Description { get; set; } = null!;
        // not need int update 
        public DateTime ? ExpectedEndDate { get; set; }
        public int TagId { get; set; }  // IN SPA SHOULD HAVE LIST OF USER TAGS 
    }

}
