using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Repositories
{
    /// <summary>
    /// Temel bütün entity'lerde kullanıcağımız crud işlemlerimizin bulunduğu interface'dir.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepostiory<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        //IQuarebla<T>  => repostitory.Where(x=>x.id ==5).toList() gibi imkanlar sağlar bize.
        /*
         *Lamda ifadesi ile sorgu kullanmamız için bu expression'u tanımlamalıyız mutlaka. 
         */
        IQueryable<T> Where(Expression<Func<T, bool>> expression);


        //repo.GetAll(x=>x.id==5) dediğimizde memorideki datadan çeker veriyi.
        //ama tolist gibi metot ile çağırırsak bunu tekrar veritabanından sorgu çeker.
        IQueryable<T> GetAll();


        Task AddAsync(T entity);

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);


        /*Update ve delete metotlarında bilindiği gibi async olmaz.
         */
        void Update(T entity);

        void Remove(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);
        void RemoveRange(IEnumerable<T> entities);
    }
}
