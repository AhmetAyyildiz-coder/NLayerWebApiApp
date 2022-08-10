﻿using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepostiory<T> where T : class
    {
        //Db islemleri icin repo katmanında dbcontext bulunur. 
        //core katmanından kalıtım aldığı için entitiler de vardır bu sayede en temel crud işlemlerini yaparız.
       
        //bunu protected yapmamızın sebebi ileride extra metotlar için kullanabiliriz. Miras alınarak
        readonly protected AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(AppDbContext context)
        {
            this._context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
           return await _dbSet.AnyAsync(expression);
        }

        public  IQueryable<T> GetAll(Expression<Func<T, bool>> expression)
        {//dataları memoriye almadan dataları izleme diyoruz. Performansı etkilememesi için.
            return  _dbSet.AsNoTracking().AsQueryable();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Remove(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            //ikisi de aynı işe yarıyor.
            //_dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }
    }
}
