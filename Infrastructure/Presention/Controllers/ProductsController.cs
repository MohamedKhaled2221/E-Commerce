using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contracts;
using Shared.Dtos;

namespace Presention.Controllers
{
    #region Part 4 Product Controller
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager serviceManager) : ControllerBase
    {
        #region Get All Products
        [HttpGet] // GET : BaseUrl/api/Products
        public async Task<ActionResult<IEnumerable<ProductResultDto>>> GetAllProducts()
        => Ok(await serviceManager.ProductService.GetAllProductsAsync());

        #endregion

        #region Get All Brands
        [HttpGet("Brands")] // GET : BaseUrl/api/Products/brands
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrands()
            => Ok(await serviceManager.ProductService.GetAllBrandsAsync());

        #endregion

        #region Get All Types
        [HttpGet("Types")] // GET : BaseUrl/api/Products
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypes()
            => Ok(await serviceManager.ProductService.GetAllTypesAsync());
        #endregion

        #region Get Product By Id
        [HttpGet("{id:int}")] // GET : BaseUrl/api/Products/{id}
        public async Task<ActionResult<ProductResultDto>> GetProductById(int id)
        {
            var product = await serviceManager.ProductService.GetProductByIdAsync(id);
            if (product is null)
                return NotFound();
            return Ok(product);
        }
        #endregion
    } 
    #endregion
}
