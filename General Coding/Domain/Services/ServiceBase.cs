using System;
using System.Collections.Generic;
using AutoMapper;
using Framework.Domain.Interface.Services;
using Framework.Domain.Repository;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using Framework.Domain.Model;

namespace Framework.Domain.Services
{
  public class ServiceBase<TEntity, TModel> : IDisposable, IServiceBase<TEntity, TModel>
      where TEntity : class
      where TModel : class
  {
    #region Properties
    private readonly RepositoryBase<TEntity> _repository;
    #endregion

    #region Constructor
    public ServiceBase(RepositoryBase<TEntity> repository)
    {
      _repository = repository;
    }
    #endregion

    #region Heavily typed (ViewModel)
    public virtual TModel GetById(params object[] id)
    {
      return Mapper.Map<TModel>(_repository.GetById(id));
    }

    public virtual IEnumerable<TModel> GetAll(Expression<Func<TEntity, bool>> exp)
    {

      var all = _repository.GetAll(exp);
      List<TModel> final = new List<TModel>();
      foreach (var item in all)
      {
        final.Add(Mapper.Map<TModel>(item));
      }
      return final;
    }

    public virtual IEnumerable<TModel> GetAll(Expression<Func<TEntity, bool>> exp, int take)
    {
      var all = _repository.GetAll(exp, take);
      List<TModel> final = new List<TModel>();
      foreach (var item in all)
      {
        final.Add(Mapper.Map<TModel>(item));
      }
      return final;
    }

    public virtual IEnumerable<TModel> GetAll()
    {
      return Mapper.Map<IEnumerable<TModel>>(_repository.GetAll());
    }

    public virtual TModel Insert(TModel obj)
    {
      return Mapper.Map<TModel>(_repository.Insert(Mapper.Map<TEntity>(obj)));
    }

    public virtual TEntity InsertNew(TModel obj)
    {
      return _repository.Insert(Mapper.Map<TEntity>(obj));
    }

    public virtual TModel Update(TModel obj)
    {
      var current = _repository.GetById(GetPrimaryKey(obj));
      current = Mapper.Map<TModel, TEntity>(obj, current);
      return Mapper.Map<TModel>(_repository.Update(current));
    }

    public virtual TEntity UpdateNew(TModel obj)
    {
      var current = _repository.GetById(GetPrimaryKey(obj));
      current = Mapper.Map<TModel, TEntity>(obj, current);
      return _repository.Update(current);
    }

    public virtual void Delete(TModel obj)
    {
      var current = _repository.GetById(GetPrimaryKey(obj));
      current = Mapper.Map<TModel, TEntity>(obj, current);
      _repository.Delete(current);
    }

    public virtual void DeleteRemove(TModel obj)
    {
      var current = _repository.GetById(GetPrimaryKey(obj));
      current = Mapper.Map<TModel, TEntity>(obj, current);
      _repository.DeleteRemove(current);
    }

    public virtual void DeleteById(int Id)
    {
      var current = _repository.GetById(Id);
      _repository.Delete(current);
    }

    public void Dispose()
    {
      _repository.Dispose();
    }

    private object[] GetPrimaryKey(TModel obj)
    {
      var keys = obj.GetType().GetProperties()
              .Where(p => p.CustomAttributes
                  .Any(attr => attr.AttributeType == typeof(KeyAttribute)));

      return keys.Select(o => o.GetValue(obj)).ToArray();
    }

    private object[] GetPrimaryKeyPrimitiveType(TEntity obj)
    {
      var keys = obj.GetType().GetProperties()
              .Where(p => p.CustomAttributes
                  .Any(attr => attr.AttributeType == typeof(KeyAttribute)));

      return keys.Select(o => o.GetValue(obj)).ToArray();
    }
    #endregion

    #region Get primitive (Model) Types

    public virtual TEntity GetByIdPrimitiveType(params object[] id)
    {
      return _repository.GetById(id);
    }

    public virtual IEnumerable<TEntity> GetAllPrimitiveType(Expression<Func<TEntity, bool>> exp)
    {
      return _repository.GetAll(exp);
    }

    public virtual IEnumerable<TEntity> GetAllPrimitiveType(Expression<Func<TEntity, bool>> exp, int take)
    {
      return _repository.GetAll(exp, take);
    }

    public virtual IEnumerable<TEntity> GetAllPrimitiveType()
    {
      return _repository.GetAll();
    }

    public virtual TEntity InsertPrimitiveType(TEntity obj)
    {
      return _repository.Insert(obj);
    }

    public virtual TEntity UpdatePrimitiveType(TEntity obj)
    {
      return _repository.Update(obj);
    }

    public virtual void DeleteRemovePrimitiveType(TEntity obj)
    {
      _repository.DeleteRemove(obj);
    }

    public virtual void DeleteByIdPrimitiveType(int Id)
    {
      var current = _repository.GetById(Id);
      _repository.Delete(current);
    }

    #endregion
  }
}