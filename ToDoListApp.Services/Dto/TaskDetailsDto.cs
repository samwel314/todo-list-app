namespace ToDoListApp.Services.Dto
{
    public class TaskDetailsDto : TaskDto
    {
        public string Description { get; set; } = null!;
        public string CreatedAT { get; set; } = null!;
        public bool IsCompleted { get; set; }
        public string CompletedAT { get; set; } = null!;
        public string ExpectedEndDate { get; set; } = null!;

    }

}
