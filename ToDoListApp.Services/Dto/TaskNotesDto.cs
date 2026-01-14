namespace ToDoListApp.Services.Dto
{
    public class TaskNotesDto
    {
        public int TaskId { get; set; } 
        public IEnumerable<NoteDto> Notes { get; set; } = null!;        
    }
}
