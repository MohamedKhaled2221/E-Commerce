using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.ProdutModule;
using Services.Abstraction.Contracts;
using Services.Specifications;
using Shared.Dtos;

namespace Services.Implementations
{
    #region Part 2 Product Service 
    internal class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {


        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            // 1. Retrive All Brands ----> Unit of Work --> Repository --> DbContext --> Database
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            //2. Mapping From ProductBrand to BrandResultDto --> AutoMapper
            var brandsResult = _mapper.Map<IEnumerable<BrandResultDto>>(brands);
            //3. Return
            return brandsResult;

        }

        public async Task<IEnumerable<ProductResultDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(new ProductWithBrandAndTypeSpecifications());

            var productsResult = _mapper.Map<IEnumerable<ProductResultDto>>(products);

            return productsResult;

        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var typesResult = _mapper.Map<IEnumerable<TypeResultDto>>(types);
            return typesResult;
        }

        public async Task<ProductResultDto> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.GetRepository<Product, int>().GetAsync(new ProductWithBrandAndTypeSpecifications(id));
            var productResult = _mapper.Map<ProductResultDto>(product);
            return productResult;
        }
    } 
    #endregion
}
