using System;
using System.Collections.Generic;

namespace Framework.Domain.Interface.Services
{
    public interface IServiceBase<TEntity, TModel>
        where TEntity : class
        where TModel : class
    {
        TModel GetById(params object[] id);
        IEnumerable<TModel> GetAll();
        TModel Insert(TModel obj);
        TModel Update(TModel obj);
        void Delete(TModel obj);
        void Dispose();
    }
}