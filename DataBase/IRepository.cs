using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataBase
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity this[int key] { get; set; }
        TEntity Get(int key);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void Remove(int key);
        void RemoveRange(IEnumerable<TEntity> entities);
        bool Exists(int key);
    }
}
