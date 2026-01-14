using ToDoListApp.Data.Repository.Implementation;
using ToDoListApp.Models;

namespace ToDoListApp.Data.Repository
{
    public interface IUnitOfWork 
    {
        IRepository<Tag> Tags { get; }
        IRepository<Note> Notes { get; }
        IRepository<TaskToDO> Tasks {  get; }

        void Save();
    }

}
