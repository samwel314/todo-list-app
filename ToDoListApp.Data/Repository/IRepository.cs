using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using ToDoListApp.Data.Repository.Implementation;

namespace ToDoListApp.Data.Repository
{
    public interface IRepository<T> where T : class
    {
        T ?Get(Expression<Func<T, bool>> Expression, bool Tracking = false, string Include = null!);
        IQueryable<T> GetAll (Expression<Func<T, bool>> Expression = null!, bool Tracking = false, string Include = null!);
        void Add( T entity );       
        void Delete( T entity );
    }




}
