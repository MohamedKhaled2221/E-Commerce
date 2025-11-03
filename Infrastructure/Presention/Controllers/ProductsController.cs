using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contracts;
using Shared;
using Shared.Dtos.ProductModule;
using Shared.Enums;
using Shared.Error_Models;

namespace Presention.Controllers
{
    #region Part 4 Product Controller
   
    public class ProductsController(IServiceManager serviceManager) :ApiControllerBase
    {
        #region Get All Products
        [HttpGet] // GET : BaseUrl/api/Products
        public async Task<ActionResult<PaginatedResult<ProductResultDto>>> GetAllProducts([FromQuery]ProductSpecParams parameters)

        => Ok(await serviceManager.ProductService.GetAllProductsAsync(parameters));

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

        #region Part 7 Improve Swagger Documentation
        [ProducesResponseType(typeof(ProductResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]

        #endregion
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
