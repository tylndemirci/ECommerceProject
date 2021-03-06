﻿using System;
using System.Linq;
using System.Linq.Expressions;

namespace ECommerceProject.DataAccess.Abstract
{
    public interface IEntityRepository<T> where T : class
    {
        IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null);
     
        T GetBy(Expression<Func<T, bool>> filter);
        
        void Add(T entity);
        void AddWithoutSave(T entity);
        void Update(T entity);
        void UpdateWithoutSave(T entity);
        void Delete(T entity);
        
        int Commit();
    }
}
