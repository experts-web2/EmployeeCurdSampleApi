using Domian.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DAL.GenericRepo
{
    public interface IGenericRepository<T> where T : EntityBase
    {
        IQueryable<T> GetAll();
        Task<T> Add(T item);
        void Delete(int id);
        T update(T item);
        Task<T?> GetByID(int id);
        IQueryable<T> Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        int SaveChange();
        Task<int> SaveChangeAsync();
    }
}
