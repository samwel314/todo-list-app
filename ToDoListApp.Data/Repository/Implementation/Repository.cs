using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ToDoListApp.Data.Repository.Implementation
{
    public class Repository<T>: IRepository<T> where T : class
    {
        private readonly AppDBContext _db;
        private readonly DbSet<T> _dbSet;
        public Repository(AppDBContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();  
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _db.Remove(entity); 
        }

        public T ? Get(Expression<Func<T, bool>> Expression, bool Tracking = false, string Include = null!)
        {
            IQueryable<T> query = _dbSet; 
            if (!Tracking)
                query = _dbSet.AsNoTracking(); 

            if (Include != null)
            {
                foreach (var item in Include.Split(','))
                {
                    query = query.Include(item); 
                }
            }
            return query.FirstOrDefault(Expression);  
        }
       public IQueryable<T> GetAll(Expression<Func<T, bool>> Expression =null!, bool Tracking = false, string Include = null!)
       {
            IQueryable<T> query = _dbSet;
            if (!Tracking)
                query = _dbSet.AsNoTracking();

            if (Include != null)
            {
                foreach (var item in Include.Split(','))
                {
                    query = query.Include(item);
                }
            }
            if (Expression == null )
                return query;
            return query.Where(Expression);
        }

    }

}
