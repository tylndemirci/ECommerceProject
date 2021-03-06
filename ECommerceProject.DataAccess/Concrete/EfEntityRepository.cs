﻿using System;
using System.Linq;
using System.Linq.Expressions;
using ECommerceProject.DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;

namespace ECommerceProject.DataAccess.Concrete
{
    public class EfEntityRepository<T> : IEntityRepository<T> where T : class
    {
        private readonly ECommerceProjectContext _dbContext;

        public EfEntityRepository(ECommerceProjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            return _dbContext.Set<T>().AsNoTracking();
            //asnotracking added, in case of a problem, delete it.

        }

       

        public T GetBy(Expression<Func<T, bool>> filter)
        {
            return _dbContext.Set<T>().FirstOrDefault(filter);
        }

        public void Add(T entity)
        {
            var addedEntity = _dbContext.Entry(entity);
            addedEntity.State = EntityState.Added;
            _dbContext.SaveChanges();
        }
         public void AddWithoutSave(T entity)
         {
             var addedEntity = _dbContext.Entry(entity);
              addedEntity.State = EntityState.Added;
                  
         }

        public void Update(T entity)
        {
            var updatedEntity = _dbContext.Entry(entity);
            
            updatedEntity.State = EntityState.Modified;
            _dbContext.SaveChanges();
            

        }

        public void UpdateWithoutSave(T entity)
        {
            var updatedEntity = _dbContext.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            
        }

        public void Delete(T entity)
        {
            var deletedEntity = _dbContext.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            _dbContext.SaveChanges();
        }


        public int Commit()
        {
            
            try
            {
                var i = _dbContext.SaveChanges();
                return i;
            }
            catch (Exception)
            {
                return -1;
            }


        }
    }
}
