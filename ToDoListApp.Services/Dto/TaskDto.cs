using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ToDoListApp.Services.Dto
{
    public class TaskDto
    {
        public int TaskId { get; set; }
        public string Title { get; set; } = null!;
        public string Status { get; set; } = null!; 
        public string TagName { get; set; } = null!;
    }
}
