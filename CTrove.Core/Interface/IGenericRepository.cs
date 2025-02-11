using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> GetAll();
        DbSet<T> GetDbSet();
        Task Add(T Entity);
        Task Update(T Entity);
        Task Deactivate(T Entity);
        IQueryable<T> FindAll();

        Task<IEnumerable<T>> FindAllByCondition(Expression<Func<T, bool>> condition);
        Task<T> FindByCondition(Expression<Func<T, bool>> condition);
    }
}
