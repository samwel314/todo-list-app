namespace ToDoListApp.Services.Dto
{
    public class CreateUpdateNoteDto
    {
        public string Progress_Note { get; set; } = null!;
        public int TaskId { get; set; }
    }   
}
