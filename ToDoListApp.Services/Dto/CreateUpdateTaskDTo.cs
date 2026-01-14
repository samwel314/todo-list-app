namespace ToDoListApp.Services.Dto
{
    public class CreateUpdateTaskDTo
    {
        // we will add user id 
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        // not need int update 
        public DateTime ? ExpectedEndDate { get; set; }
        public int TagId { get; set; }  // IN SPA SHOULD HAVE LIST OF USER TAGS 
    }

}
