using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Framework.Database.Context;
using Framework.Domain.Interface.Repository;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Validation;

namespace Framework.Domain.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : class
    {
        protected ApplicationDbContext Db;
        protected DbSet<TEntity> DbSet;

        public RepositoryBase()
        {
            Db = new ApplicationDbContext();
            DbSet = Db.Set<TEntity>();
        }

        public virtual TEntity GetById(params object[] id)
        {
            return DbSet.Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> exp)
        {
            return DbSet.Where(exp).ToList();
        }

        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> exp, int take)
        {
            return DbSet.Where(exp).Take(take).ToList();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return DbSet.ToList();
        }

        public virtual IEnumerable<TEntity> GetAllFastest()
        {
            return DbSet;
        }

        public virtual TEntity Insert(TEntity obj)
        {
            try
            {
                DbSet.Add(obj);
                Db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            
            return obj;
        }

        public virtual TEntity Update(TEntity obj)
        {
            var entry = Db.Entry(obj);
            DbSet.Attach(obj);
            entry.State = EntityState.Modified;
            Db.SaveChanges();

            return obj;
        }

        public virtual void Delete(TEntity obj)
        {
            var entry = Db.Entry(obj);
            DbSet.Attach(obj);
            entry.State = EntityState.Deleted;
            Db.SaveChanges();
        }

        public virtual void DeleteRemove(TEntity obj)
        {
            var entry = Db.Entry(obj);
            entry.State = EntityState.Deleted;
            DbSet.Remove(obj);
            Db.SaveChanges();
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
         
    }
}