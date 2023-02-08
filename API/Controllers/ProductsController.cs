using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController: ControllerBase
    {
        private readonly IProductRepository _repo;
        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts() => Ok(await _repo.GetProductsAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id) => await _repo.GetProductByIdAsync(id);

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes() => Ok(await _repo.GetProductTypesAsync());

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands() => Ok(await _repo.GetProductBrandsAsync());
    }
}