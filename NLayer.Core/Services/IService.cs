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


       
        Task<IEnumerable<T>> GetAll();

        Task<T> AddAsync(T entity);

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

        Task UpdateAsync(T entity);

        Task RemoveAsync(T entity);

        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        Task RemoveRangeAsync(IEnumerable<T> entities);
    }
}
