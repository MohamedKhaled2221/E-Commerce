using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Abstraction.Contracts;

namespace Services.Implementations
{
    #region Part 1 ServiceManager With Factory Delegate
    public class ServiceManagerWithFactoryDelegate
(Func<IProductService> _productFactory, Func<IAuthenticationService> _authenticationFactory,
Func<IBasketService> _basketFactory, Func<IPaymentService> _paymentFactory
, Func<IOrderService> _orderFactory,Func<ICacheService> _cacheFactory) : IServiceManager
    {

        public IProductService ProductService => _productFactory.Invoke();

        public IBasketService BasketService => _basketFactory.Invoke();

        public IAuthenticationService AuthenticationService => _authenticationFactory.Invoke();

        public IOrderService OrderService => _orderFactory.Invoke();

        public IPaymentService PaymentService => _paymentFactory.Invoke();

        public ICacheService CacheService => _cacheFactory.Invoke() ;
    } 
    #endregion
}
