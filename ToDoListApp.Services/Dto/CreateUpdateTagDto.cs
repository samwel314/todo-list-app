namespace ToDoListApp.Services.Dto
{
    public class CreateUpdateTagDto
    {
        public string TagName { get; set; } = null!;
        // fetch from api call token claims
        public string ? UserId { get; set; } = null!; 
    }
}
