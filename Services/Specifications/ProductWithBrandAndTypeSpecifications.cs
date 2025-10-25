using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.ProdutModule;

namespace Services.Specifications
{
    #region Part 9 Specifications [Refactor GetAllProducts & GetProductById]
    internal class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product, int>
    {
        // Get all Products
        //  // query = _dbContext.Set<Product>().Include(p => p.ProductBrand).Include(p => p.ProductType)
        public ProductWithBrandAndTypeSpecifications() : base(null)
        {
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);

        }
        //Ctor for  Get Product by Id
        public ProductWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id)
        {
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);

        }
    } 
    #endregion
}
