using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using ToDoListApp.Models;
using ToDoListApp.Services.Dto;

namespace ToDoListApp.Services
{
    public interface INoteService
    {
        TaskNotesDto ? GetTaskNotes(int taskId);
        NoteDto? Get(int id);
        bool Delete(int id);
        int Create( int taskId , CreateUpdateNoteDto dto);   
        bool Update(int id, CreateUpdateNoteDto dto);   
    }
}
