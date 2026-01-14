namespace ToDoListApp.Services.Dto
{
    public class NoteDto
    {
        public int NoteId { get; set; }
        public string Progress_Note { get; set; } = null!;
        public string CreatedOrUpdatedAt { get; set; } = null!;
    }
}
