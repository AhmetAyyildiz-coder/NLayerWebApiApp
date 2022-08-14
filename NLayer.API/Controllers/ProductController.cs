using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    
    public class ProductController : CustomBaseController
    {//controller'lar sadece servisleri bilir. Kesinlikle repository'yi referans almazlar.

        private readonly IMapper _mapper;
        //private readonly IService<Product> _service;
        private readonly IProductService _service;
        public ProductController(IMapper mapper , IProductService service)
        {
            _mapper = mapper;
            _service = service;
        }



        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _service.GetAll();
            var productDtos = _mapper.Map<List<ProductDto>>(products).ToList();
            //return Ok(CustomReponseDto<List<ProductDto>>.Success(200, productDtos));
            return CreateActionResult(CustomReponseDto<List<ProductDto>>.Success(200, productDtos));
        }
        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> getById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            var productDtos = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomReponseDto<ProductDto>.Success(200, productDtos));

        }
        /*Fluent validation'da eğer bu controller'a gelen productDto nesnesi 
         validation'a takılırsa metodun içine girmiyor. Direkt return oluyor.*/
        [HttpPost()]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var product = await _service.AddAsync(_mapper.Map<Product>(productDto));
            var _productDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomReponseDto<ProductDto>.Success(201, _productDto));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            await _service.UpdateAsync(_mapper.Map<Product>(productDto));
            return CreateActionResult(CustomReponseDto<ProductDto>.Success(204));
        }
        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(product);
           
            return CreateActionResult(CustomReponseDto<ProductDto>.Success(204));
        }
        [HttpGet("GetProductsWithCategory")]
        public async Task<IActionResult> GetProductsWithCategory()
        {
            
            return CreateActionResult(await _service.GetProductsWithCategory());
        }

    }
}
