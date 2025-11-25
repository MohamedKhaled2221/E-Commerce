using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Domain.Contracts;
using Domain.Entities.BasketModule;
using Domain.Entities.OrderModule;
using Domain.Entities.ProdutModule;
using Domain.Exceptions;
using Services.Abstraction.Contracts;
using Services.Specifications;
using Shared.Dtos.OrderModule;

namespace Services.Implementations
{

    #region Part 5 Order Service Create Order 
    internal class OrderService(IMapper mapper , 
    IBasketRepository basketRepository, IUnitOfWork unitOfWork) : IOrderService

    {
        #region Part 3 Refactor Create Order & OrderWithPaymentSpecifications
        public async Task<OrderResult> CreateOrderAsync(OrderRequest request, string userEmail)
        {

            // 1 . Shpping Address
            var address = mapper.Map<ShippingAddress>(request.ShippingAddress);


            // 2. Order Items => BaskedId  --> BasketItems --> OrderItems
            var basket = await basketRepository.GetBasketAsync(request.BasketId) ?? throw new BasketNotFoundException(request.BasketId);


            // Get selected Items at Basket from Product Repository
            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = await unitOfWork.GetRepository<Product, int>()
                     .GetAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);
                orderItems.Add(CreateOrderItem(item, product));

            }

            // 3. Delivery Method  
            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetAsync(request.DeliveryMethodId) ?? throw new DeliveryMethodNotFoundException(request.DeliveryMethodId);

            // 4. Subtotal --> item.Price * item.Quantity
            var orderrepo = unitOfWork.GetRepository<Order, Guid>();

            var exsistingOrder = await orderrepo
                .GetAsync(new OrderWithPaymentIntentSpecifications(basket.PaymentIntentId!));

            if (exsistingOrder != null)
                orderrepo.Delete(exsistingOrder);

            var subtotal = orderItems.Sum(item => item.Price * item.Quantity);

            // 5 . Create Order

            var order = new Order(userEmail, address, orderItems, deliveryMethod, subtotal, basket.PaymentIntentId!);

            //  Save to Database

            await orderrepo
                        .AddAsync(order);
            await unitOfWork.SaveChangesAsync();
            // Map from Order to OrderResult and return
            return mapper.Map<OrderResult>(order);
        } 
        #endregion

        private OrderItem CreateOrderItem(BasketItem item, Product product)
        => new OrderItem(new ProductinOrderItem(product.Id, product.Name, product.PictureUrl), item.Quantity, product.Price);
        #endregion
        #region Part 6 Order Service [ Get Delivery Methods ] 
        public async Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods = await unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetAllAsync();
            return mapper.Map<IEnumerable<DeliveryMethodResult>>(deliveryMethods);
        }
        #endregion

        #region Part 7 Order Service & Order Specifications
        public async Task<OrderResult> GetOrderByIdAsync(Guid id)
        {
            var order = await unitOfWork.GetRepository<Order, Guid>()
                 .GetAsync(new OrderWithIncludeSpecifications(id)) ?? throw new OrderNotFoundException(id);

            return mapper.Map<OrderResult>(order);
        }

        // Order.Email == email 
        public async Task<IEnumerable<OrderResult>> GetOrdersByEmailAsync(string email)
        {
            var orders = await unitOfWork.GetRepository<Order, Guid>()
                 .GetAllAsync(new OrderWithIncludeSpecifications(email));

            return mapper.Map<IEnumerable<OrderResult>>(orders);
        } 
        #endregion
    } 

  
}
