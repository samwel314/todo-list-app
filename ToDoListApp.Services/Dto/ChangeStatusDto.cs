namespace ToDoListApp.Services.Dto
{
    public class ChangeStatusDto
    {
        public DateTime ?ExpectedEndDate { get; set; } = null!;
        public Models.TaskStatus ? NewStatus { get; set; } 

    }

}
