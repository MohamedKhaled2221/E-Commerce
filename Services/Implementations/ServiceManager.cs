using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Services.Abstraction.Contracts;
using Shared;

namespace Services.Implementations
{
    #region Part 3 Service Manager
    public class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper , IBasketRepository basketRepository, 
        UserManager<User> userManager, IOptions<JwtOptions> options) : IServiceManager
    {
        private readonly Lazy<IProductService> _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
        private readonly Lazy<IBasketService> _basketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, mapper));
        private readonly Lazy<IAuthenticationService> _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager,options,mapper));
       private readonly Lazy<IOrderService> _orderService = new Lazy<IOrderService>(() => new OrderService(mapper,basketRepository,unitOfWork));

        public IProductService ProductService => _productService.Value;

        public IBasketService BasketService => _basketService.Value;

        public IAuthenticationService AuthenticationService => _authenticationService.Value;

        public IOrderService OrderService => _orderService.Value;
    } 
    #endregion
}
