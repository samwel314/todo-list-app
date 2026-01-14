using System;
using System.Collections.Generic;
using System.Text;
using ToDoListApp.Models;

namespace ToDoListApp.Data.Repository.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDBContext _dbContext;
        public UnitOfWork(AppDBContext dBContext) 
        {
            _dbContext = dBContext; 
            Tags = new Repository<Tag>(_dbContext);
            Notes = new Repository<Note>(_dbContext); 
            Tasks = new Repository<TaskToDO>(_dbContext); 
        }

        public IRepository<Tag> Tags {  get; private set; }
        public IRepository<Note> Notes { get; private set; }
        public IRepository<TaskToDO> Tasks { get; private set; }

        public void Save()
        {
            _dbContext.SaveChanges();   
        }
    }
}
