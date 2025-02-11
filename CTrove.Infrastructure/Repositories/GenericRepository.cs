using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CTrove.Core.Interface;
using Microsoft.EntityFrameworkCore;

namespace CTrove.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly CtroveDatabaseContext _dbContext;
        public GenericRepository(CtroveDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<T> GetById(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
        public DbSet<T> GetDbSet() => _dbContext.Set<T>();

        public async Task Add(T Entity)
        {
            await _dbContext.Set<T>().AddAsync(Entity);
        }
        public async Task Update(T Entity)
        {
             _dbContext.Set<T>().Update(Entity);
        }
        public async Task Deactivate(T Entity)
        {
            //_dbContext.Set<T>().Remove(Entity);
            _dbContext.Set<T>().Update(Entity);
        }
        public IQueryable<T> FindAll()
        {
            return _dbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> FindAllByCondition(Expression<Func<T, bool>> condition)
        {
            return await _dbContext.Set<T>().Where(condition).ToListAsync();
        }

        public async Task<T> FindByCondition(Expression<Func<T, bool>> condition)
        {
            return await _dbContext.Set<T>().Where(condition).FirstOrDefaultAsync();
        }

    }
}
