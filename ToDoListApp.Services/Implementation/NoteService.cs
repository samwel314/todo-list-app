using System.Threading.Tasks;
using ToDoListApp.Data.Repository;
using ToDoListApp.Models;
using ToDoListApp.Services.Dto;

namespace ToDoListApp.Services.Implementation
{
    public class NoteService : INoteService
    {
        private readonly IUnitOfWork _db;

        public NoteService(IUnitOfWork db)
        {
            _db = db;
        }

        public int Create(int taskId, CreateUpdateNoteDto dto)
        {
            var inDB =
              _db.Tasks.GetAll(t => t.TaskId == taskId).Any();
            if (!inDB)
                return 0;
            var note = new Note()
            {
                Progress_Note = dto.Progress_Note,
                CreatedOrUpdatedAt = DateTime.Now,
                TaskId = taskId,
            }; 
            _db.Notes.Add(note);
            _db.Save();
            return note.NoteId;
        }

        public bool Delete(int id)
        {
            Note ? noteFromDB = _db.Notes.Get(n => n.NoteId == id);    
            if (noteFromDB == null)
                return false;
            _db.Notes.Delete(noteFromDB);     
            _db.Save(); 
            return true;
        }
        public NoteDto? Get(int id)
        {
            Note? noteFromDB = _db.Notes.Get(n => n.NoteId == id);
            if (noteFromDB == null)
                return null;
            var noteDto = new NoteDto()
            {
                NoteId = noteFromDB.NoteId,
                Progress_Note = noteFromDB.Progress_Note,
                CreatedOrUpdatedAt = noteFromDB.CreatedOrUpdatedAt.ToShortDateString(),
            }; 
            return noteDto; 
        }
        public TaskNotesDto ? GetTaskNotes(int taskId)
        {
            var inDB = 
                _db.Tasks.GetAll(t => t.TaskId == taskId).Any();
            if (!inDB)
                return null;
            
            var notesFromDB = 
                _db.Notes.GetAll(n => n.TaskId == taskId)
                .Select(n => new NoteDto()
                {
                    NoteId = n.NoteId,
                    Progress_Note = n.Progress_Note,
                    CreatedOrUpdatedAt = n.CreatedOrUpdatedAt.ToString(),
                }).ToList();
           
            TaskNotesDto taskNotesDto = new TaskNotesDto()
            {
                TaskId = taskId,
                Notes = notesFromDB,
            };
          
            return taskNotesDto;
        }

        public bool Update(int id, CreateUpdateNoteDto dto)
        {
            var noteFromDB =
                _db.Notes
                .Get(t => t.NoteId == id , true); 
            if (noteFromDB == null || noteFromDB.TaskId != dto.TaskId)
                return false;
            noteFromDB.Progress_Note = dto.Progress_Note;
            noteFromDB.CreatedOrUpdatedAt = DateTime.Now;
            _db.Save(); 
            return true;
        }
    }
}
