using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Services.Exceptions;

namespace Nlayer.Caching;

public class ProductServiceWithCaching :IProductService
{
    private const string CacheProductKey = "productsCache";
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;
    private readonly IProductRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductServiceWithCaching(IMapper mapper, IMemoryCache memoryCache, IProductRepository repository, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _memoryCache = memoryCache;
        _repository = repository;
        _unitOfWork = unitOfWork;

        if (!_memoryCache.TryGetValue(CacheProductKey, out _))
        {
            _memoryCache.Set(CacheProductKey, _repository.GetProductsWithCategory().Result);
        }

    }

    public Task<Product> GetByIdAsync(int id)
    {
        var product = _memoryCache.Get<List<Product>>(CacheProductKey).FirstOrDefault(x => x.Id == id);

        if (product == null)
        {
            throw new NotFoundException($"{typeof(Product).Name}({id}) not found");
        }

        return Task.FromResult(product);
    }

    public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
    {
        
        return this._memoryCache.Get<List<Product>>(CacheProductKey).
            Where(expression.Compile())
            .AsQueryable();
    }

    public Task<IEnumerable<Product>> GetAll()
    {
        var products = _memoryCache.Get<IEnumerable<Product>>(CacheProductKey);
        return Task.FromResult(products);
    }

    public async Task<Product> AddAsync(Product entity)
    {
        await _repository.AddAsync(entity);
        await _unitOfWork.CommitAsync();
        await CacheAllProducts();
        return entity;
    }

    public async Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
    {
        /*
         * Expression<Func<T,boo>> => convert to Func<T,bool>
         * Expression.Compile = Func
         */
        return   this._memoryCache.Get<List<Product>>(CacheProductKey)
            .Any(expression.Compile());
    }
    

    public async Task UpdateAsync(Product entity)
    {
        _repository.Update(entity);
        await _unitOfWork.CommitAsync();
        await this.CacheAllProducts();
    }

    public async Task RemoveAsync(Product entity)
    {
        _repository.Remove(entity);
        await _unitOfWork.CommitAsync();
        await this.CacheAllProducts();
    }

    public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
    {
        await _repository.AddRangeAsync(entities);
        await _unitOfWork.CommitAsync();
        await this.CacheAllProducts();
        return entities;
    }

    public async Task RemoveRangeAsync(IEnumerable<Product> entities)
    {
        _repository.RemoveRange(entities);
        await _unitOfWork.CommitAsync();
        await this.CacheAllProducts();
        
    }

    public async Task<CustomReponseDto<List<ProductWithCategory>>> GetProductsWithCategory()
    {
        var product =  _memoryCache.Get<IEnumerable<Product>>(CacheProductKey);
        var productWithCategoryDto = _mapper.Map<List<ProductWithCategory>>(product);
        return CustomReponseDto<List<ProductWithCategory>>.Success(200, productWithCategoryDto);
    }

    public async Task CacheAllProducts()
    {
        _memoryCache.Set(CacheProductKey, await _repository.GetAll().ToListAsync());
    }
}