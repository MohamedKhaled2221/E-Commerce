using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Services.Abstraction.Contracts;

namespace Services.Implementations
{
    #region Part 3 Service Manager
    public class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper , IBasketRepository basketRepository) : IServiceManager
    {
        private readonly Lazy<IProductService> _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
        private readonly Lazy<IBasketService> _basketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, mapper));
        public IProductService ProductService => _productService.Value;

        public IBasketService BasketService => _basketService.Value;
    } 
    #endregion
}
