using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    /// <summary>
    /// Projede kullanılacak interface icin bir şablon oluşturmamızı sağlar.
    /// </summary>
    public interface IService<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
       
        IQueryable<T> Where(Expression<Func<T, bool>> expression);


       
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression);

        Task AddAsync(T entity);

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

        Task UpdateAsync(T entity);

        Task RemoveAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);
        Task RemoveRangeAsync(IEnumerable<T> entities);
    }
}
