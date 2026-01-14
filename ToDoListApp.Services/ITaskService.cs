using System;
using System.Collections.Generic;
using System.Text;
using ToDoListApp.Models;
using ToDoListApp.Services.Dto;

namespace ToDoListApp.Services
{
    public interface ITaskService
    {
        IEnumerable<TaskDto> GetAll();
        TaskDetailsDto ? Get(int id);
        bool Update(int id, CreateUpdateTaskDTo dto);
        int Create(CreateUpdateTaskDTo dto);
        bool ExtendTime(int id, ChangeStatusDto dto);
        bool ChangeStatus(int id, ChangeStatusDto dto);
        bool Delete(int id);
    }
}
