using System;
using System.Collections.Generic;
using System.Text;
using ToDoListApp.Data.Repository;
using ToDoListApp.Models;
using ToDoListApp.Services.Dto;

namespace ToDoListApp.Services.Implementation
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _db;
        public TaskService(IUnitOfWork db)
        {
            _db = db;
        }
        public int Create(CreateUpdateTaskDTo dto , string userId)
        {
            var tagFromDb = _db.Tags.GetAll(t => t.TagId == dto.TagId && t.UserId == userId).Any();
            if (!tagFromDb)
                return 0; 
            var taskToDB = new TaskToDO()
            {
                Title = dto.Title,
                Description = dto.Description,
                TagId = dto.TagId,
                UserId = userId,
                ExpectedEndDate = dto.ExpectedEndDate!.Value,
            };
            _db.Tasks.Add(taskToDB);
            _db.Save();
            return taskToDB.TaskId;
        }
        public bool Update(int id ,  CreateUpdateTaskDTo dto)
        {
            TaskToDO? taskFromDB =
            _db.Tasks.Get(t => t.TaskId == id, true);
            if (taskFromDB == null)
                return false;
            taskFromDB.Title = dto.Title;   
            taskFromDB.Description = dto.Description;
            taskFromDB.TagId = dto.TagId;   
            _db.Save();
            return true;    
        }
        public TaskDetailsDto? Get(int id , string userId)
        {
            var taskFromDB = _db.Tasks.Get
                (t => t.TaskId == id && t.UserId == userId , Include :"Tag");
            if (taskFromDB == null) 
                return null;    
            var taskDto = new TaskDetailsDto()
            {
                TaskId = taskFromDB.TaskId,
                TagName = taskFromDB.Tag.TagName , 
                Title = taskFromDB.Title ,  
                Status = taskFromDB.Status.ToString(),  
                Description = taskFromDB.Description ,  
                CreatedAT = taskFromDB.CreatedAT.ToShortDateString(),
                ExpectedEndDate = taskFromDB.ExpectedEndDate.ToShortDateString(),
            };
            if (taskFromDB.IsCompleted)
                taskDto.CompletedAT = taskFromDB.CompletedAT!.Value.ToShortDateString();
            else
                taskDto.CompletedAT = $"This Task is {taskDto.Status}";

            return taskDto; 
        }
        public IEnumerable<TaskDto> GetAll(string userId)
        {
           var tasksFromDb =  _db.Tasks.GetAll(t=>t.UserId == userId).Select(t=> new TaskDto
            {
                TaskId = t.TaskId,  
                Title = t.Title,    
                Status = t.Status.ToString(),
                TagName = t.Tag.TagName,    
            });
            return tasksFromDb.ToList();
        }
        public bool ExtendTime (int id , ChangeStatusDto dto )
        {
            TaskToDO? taskFromDB =
            _db.Tasks.Get(t => t.TaskId == id, true);
            if (taskFromDB == null ||
           taskFromDB.Status == Models.TaskStatus.Completed)
                return false;
            taskFromDB.ExpectedEndDate = dto.ExpectedEndDate!.Value;
            if (taskFromDB.Status == Models.TaskStatus.TimeOut)
                taskFromDB.Status = Models.TaskStatus.InProgress;
            _db.Save();
            return true; 
        }
        public bool ChangeStatus (int id, ChangeStatusDto dto)
        {
            TaskToDO? taskFromDB =
                _db.Tasks.Get(t => t.TaskId == id, true);
            // user cannot change status of completed task or set any status to timeout the system handles timeout
            if (taskFromDB == null ||
                taskFromDB.Status == Models.TaskStatus.Completed 
                || dto.NewStatus!.Value == Models.TaskStatus.TimeOut)
                return false;

            if (dto.NewStatus != Models.TaskStatus.Completed)
            {
                if (dto.NewStatus!.Value < taskFromDB.Status)
                    return false;
            }
            else
            {
                taskFromDB.CompletedAT = DateTime.Now;
                taskFromDB.IsCompleted = true;     
            }
            
            taskFromDB.Status = dto.NewStatus!.Value;
            _db.Save();
            return true;    
        }
        public bool Delete(int id)
        {
            TaskToDO? taskFromDB =
                _db.Tasks.Get(t => t.TaskId == id);
            if (taskFromDB == null)
                return false;
            _db.Tasks.Delete(taskFromDB); 
            _db.Save(); 
            return true;
        }

    }
    /*
 
--------------------------------------------------------
| Status     | Visible Buttons / Actions               |
| ---------- | --------------------------------------- |
| NotStarted | Start, Complete (optional), Extend Time |
| InProgress | Complete, Extend Time                   |
| TimeOut    | Complete, Extend Time // >  InProgress  |
| Completed  | No action buttons (maybe View only)     |
--------------------------------------------------------
 */

}
