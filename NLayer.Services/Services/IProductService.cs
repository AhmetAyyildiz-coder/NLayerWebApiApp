using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Services.Services
{
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

      

        public ProductService(IUnitOfWork unitOfWork, IProductRepository repository,  IMapper mapper) : base(unitOfWork, repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CustomReponseDto<List<ProductWithCategory>>> GetProductsWithCategory()
        {
            var products = await _repository.GetProductsWithCategory();
            var productsDto = _mapper.Map<List<ProductWithCategory>>(products);
            return CustomReponseDto<List<ProductWithCategory>>.Success(200,productsDto);
        }
    }
}
