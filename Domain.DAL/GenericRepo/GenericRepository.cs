using Domian.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DAL.GenericRepo
{
    public class GenericRepository<T> : IGenericRepository<T> where T : EntityBase, new()
    {

        private readonly AppDbContext _appDbContext;
        private DbSet<T> _dbSet;
        public GenericRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = appDbContext.Set<T>();
        }

        public async Task<T> Add(T item)
        {
            var entity = await _dbSet.AddAsync(item);
            return entity.Entity;
        }

        public void Delete(int id)
        {
            var entity = _dbSet.FirstOrDefault(x => x.Id == id);
            _dbSet.Remove(entity);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var resultQueryable = _dbSet.Where(predicate);

            if (includes != null && includes.Length > 0)
                foreach (var expression in includes)
                    resultQueryable.Include(expression);

            return resultQueryable;
        }

        public IQueryable<T> GetAll()
        => _dbSet.AsNoTracking<T>();

        public async Task<T?> GetByID(int id)
        => await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

        public T update(T item)
        {
            var entity = _dbSet.Update(item);
            return entity.Entity;
        }

        public async Task<int> SaveChangeAsync()
            => await _appDbContext.SaveChangesAsync();

        public int SaveChange()
            => _appDbContext.SaveChanges();

    }
}
