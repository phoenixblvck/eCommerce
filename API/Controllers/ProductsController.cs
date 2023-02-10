using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Core.Specifications;
using AutoMapper;
using API.Dtos;
using API.Errors;
using API.Helpers;

namespace API.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IMapper _mapper;
        public ProductsController(IGenericRepository<Product> productRepo, IGenericRepository<ProductType> productTypeRepo, IGenericRepository<ProductBrand> productBrandRepo, IMapper mapper)
        {
            _mapper = mapper;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
            _productRepo = productRepo;
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
            var countSpec = new ProductsWithFiltersForCountSpecification(productParams);
            var product = await _productRepo.ListAsync(spec);
            var totalItems = await _productRepo.CountAsync(countSpec);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(product);
            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productRepo.GetEntityWithSpec(spec);
            if (product == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(_mapper.Map<Product, ProductToReturnDto>(product));

        }

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes() => Ok(await _productTypeRepo.ListAllAsync());

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands() => Ok(await _productBrandRepo.ListAllAsync());
    }
}