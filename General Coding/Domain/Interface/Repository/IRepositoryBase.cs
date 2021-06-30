using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Framework.Domain.Interface.Repository
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : class
    {
        TEntity GetById(params object[] id);
        IEnumerable<TEntity> GetAll();
        TEntity Insert(TEntity obj);
        TEntity Update(TEntity obj);
        void Delete(TEntity obj);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    }
}