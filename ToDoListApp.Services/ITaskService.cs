using System;
using System.Collections.Generic;
using System.Text;
using ToDoListApp.Models;
using ToDoListApp.Services.Dto;

namespace ToDoListApp.Services
{
    public interface ITaskService
    {
        IEnumerable<TaskDto> GetAll(string userId);
        TaskDetailsDto ? Get(int id , string userId);
        bool Update(int id, CreateUpdateTaskDTo dto , string userId);
        int Create(CreateUpdateTaskDTo dto , string userId);
        bool ExtendTime(int id, ChangeStatusDto dto , string userId);
        bool ChangeStatus(int id, ChangeStatusDto dto, string userId);
        bool Delete(int id , string userId);
    }
}
