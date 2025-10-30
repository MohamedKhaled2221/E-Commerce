using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.ProdutModule;
using Shared;
using Shared.Enums;

namespace Services.Specifications
{
    #region Part 9 Specifications [Refactor GetAllProducts & GetProductById]
    internal class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product, int>
    {
        // Get all Products
        //  // query = _dbContext.Set<Product>().Include(p => p.ProductBrand).Include(p => p.ProductType)
        #region Part 2 Add Filtering Specifications
        #region Part 7 Add Search To GET All Products 
        public ProductWithBrandAndTypeSpecifications(ProductSpecParams parameters)
         : base(product =>
         (!parameters.TypeId.HasValue || product.TypeId == parameters.TypeId.Value)
         && (!parameters.BrandId.HasValue || product.BrandId == parameters.BrandId.Value) &&
             (string.IsNullOrEmpty(parameters.Search) || product.Name.ToLower().Contains
              (parameters.Search.ToLower().Trim()))) 
        #endregion

        #endregion

        {
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);

            switch (parameters.Sort)
            {
                case ProductSortOptions.NameAsc:
                    SetOrderBy(p => p.Name);
                    break;
                    case ProductSortOptions.NameDesc:
                    SetOrderByDescending(p => p.Name);
                    break;
                    case ProductSortOptions.PriceAsc:
                    SetOrderBy(p => p.Price);
                    break;
                    case ProductSortOptions.PriceDesc:
                    SetOrderByDescending(p => p.Price);
                    break;
                default:
                    SetOrderBy(p=>p.Name);
                    break;
            }

            ApplyPagination(parameters.PageIndex, parameters.PageSize);

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
