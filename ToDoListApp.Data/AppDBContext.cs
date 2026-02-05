using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoListApp.Models;

namespace ToDoListApp.Data
{
    public class AppDBContext : IdentityDbContext<User>
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {

        }



        public DbSet<TaskToDO> Tasks { get; set; }
        public DbSet<Note> Notes { get; set; }

        public DbSet<Tag> Tags { get; set; }        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskToDO>().HasData(
                new TaskToDO
                {
                    TaskId = 1,
                    Title = "Learn EF Core",
                    Description = "Understand DbContext, Migrations, and LINQ",
                    Status = Models.TaskStatus.NotStarted,
                    CreatedAT = new DateTime(2025, 1, 1),
                    IsCompleted = false,
                    ExpectedEndDate = new DateTime(2025, 1, 10),
                    TagId = 2,  
                    
                },
                new TaskToDO
                {
                    TaskId = 2,
                    Title = "Build ToDo API",
                    Description = "Create CRUD endpoints with pagination and filtering",
                    Status = Models.TaskStatus.InProgress,
                    CreatedAT = new DateTime(2024, 12, 30),
                    IsCompleted = false,
                    ExpectedEndDate = new DateTime(2025, 1, 5),
                    TagId = 3,
                },
                new TaskToDO
                {
                    TaskId = 3,
                    Title = "Write Unit Tests",
                    Description = "Test TaskService methods using xUnit",
                    Status = Models.TaskStatus.Completed,
                    CreatedAT = new DateTime(2024, 12, 20),
                    IsCompleted = true,
                    CompletedAT = new DateTime(2024, 12, 28),
                    ExpectedEndDate = new DateTime(2024, 12, 28),
                    TagId = 4,
                },
                new TaskToDO
                {
                    TaskId = 4,
                    Title = "Refactor Repository",
                    Description = "Make generic repository work with Task and Note",
                    Status = Models.TaskStatus.TimeOut,
                    CreatedAT = new DateTime(2024, 12, 15),
                    IsCompleted = false,
                    ExpectedEndDate = new DateTime(2024, 12, 20),
                    TagId = 5,
                }
            );

            modelBuilder.Entity<Note>().HasData(
                new Note
                {
                    NoteId = 1,
                    TaskId = 1,
                    Progress_Note = "Started reading EF Core documentation",
                    CreatedOrUpdatedAt = new DateTime(2025, 1, 2, 10, 0, 0)
                },
                new Note
                {
                    NoteId = 2,
                    TaskId = 2,
                    Progress_Note = "Implemented GetAll with pagination",
                    CreatedOrUpdatedAt = new DateTime(2025, 1, 3, 12, 0, 0)
                },
                new Note
                {
                    NoteId = 3,
                    TaskId = 3,
                    Progress_Note = "All unit tests passed successfully",
                    CreatedOrUpdatedAt = new DateTime(2024, 12, 28, 18, 0, 0)
                },
                new Note
                {
                    NoteId = 4,
                    TaskId = 4,
                    Progress_Note = "Refactoring stopped because deadline missed",
                    CreatedOrUpdatedAt = new DateTime(2024, 12, 21, 9, 0, 0)
                }
            );

            modelBuilder.Entity<Tag>().HasData(
            new Tag { TagId = 1, TagName = "Work" },
            new Tag { TagId = 2, TagName = "Personal" },
            new Tag { TagId = 3, TagName = "Study" },
            new Tag { TagId = 4, TagName = "Urgent" },
            new Tag { TagId = 5, TagName = "Home" },
            new Tag { TagId = 6, TagName = "Shopping" },
            new Tag { TagId = 7, TagName = "Health" });


        }


    }
}
