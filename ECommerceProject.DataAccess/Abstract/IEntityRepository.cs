using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ECommerceProject.DataAccess.Abstract
{
    public interface IEntityRepository<T> where T : class
    {
        IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null);
     
        T GetBy(Expression<Func<T, bool>> filter);
        
        void Add(T entity);
        void Update(T entity);
        void UpdateWithoutSave(T entity);
        void Delete(T entity);
        
        int Commit();
    }
}
