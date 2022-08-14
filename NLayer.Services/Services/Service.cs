using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Services.Services
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IGenericRepository<T> _repostiory;
        private readonly IUnitOfWork _unitOfWork;

        public Service(IUnitOfWork unitOfWork, IGenericRepository<T> repostiory)
        {
            this._unitOfWork = unitOfWork;
            _repostiory = repostiory;
        }

      

        public async Task<T> AddAsync(T entity)
        {
            
            await _repostiory.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            /*Ef core tarafında add oldugu zaman eklenen entity parametre gelen entity ile yer değiştirir. 
             * Yani return ettiğimizde parametre gelen entitiy değil eklenen entity döner. Bu sayede
             işlemin başarılı oldugunu anlayabiliri.z*/
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _repostiory.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            return entities;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _repostiory.AnyAsync(expression);
        }

        public async  Task<IEnumerable<T>> GetAll()
        {
            
            return await _repostiory.GetAll().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var hasProduct = await _repostiory.GetByIdAsync(id);
            if (hasProduct == null)
            {
                 throw new NotFoundException($"{typeof(T).Name} {id} not found");
            }
            return hasProduct;
        }

        public async Task RemoveAsync(T entity)
        {
            _repostiory.Remove(entity);
            await _unitOfWork.CommitAsync();
            
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _repostiory.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _repostiory.Update(entity);
           await _unitOfWork.CommitAsync();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _repostiory.Where(expression);
        }

        
    }
}
