using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contracts;
using Shared.Dtos.OrderModule;
using System.Security.Claims;

namespace Presention.Controllers
{
    [Authorize]
    #region Part 9 Orders Controller 
    public class OrdersController(IServiceManager serviceManager) : ApiControllerBase
    {
        // Create Order 
        [HttpPost] // POST : BaseUrl/api/orders
        public async Task<ActionResult<OrderResult>> Create(OrderRequest Request)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var order = await serviceManager.OrderService.CreateOrderAsync(Request, email!);

            return Ok(order);
        }

        // Get Orders 

        [HttpGet] // GET : BaseUrl/api/orders
        public async Task<ActionResult<IEnumerable<OrderResult>>> GetOrders()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var orders = await serviceManager.OrderService.GetOrdersByEmailAsync(email!);
            return Ok(orders);
        }

        // Get Order By Id
        [HttpGet("{id}")] // GET : BaseUrl/api/orders/{id}
        public async Task<ActionResult<OrderResult>> GetOrderById(Guid id)
        {
            var order = await serviceManager.OrderService.GetOrderByIdAsync(id);
            return Ok(order);
        }

        [AllowAnonymous]
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodResult>>> GetDeliveryMethods()
        {
            var deliveryMethods = await serviceManager.OrderService.GetDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }

    } 
    #endregion
}
