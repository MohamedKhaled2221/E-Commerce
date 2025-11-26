using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.BasketModule
{
    #region Part 11 Basket Module Entities
    public class CustomerBasket
    {
        // Cart --> ID , Items : Products 
        public string Id { get; set; }
        public IEnumerable<BasketItem> Items { get; set; } = [];
        public string? ClientSecret { get; set; }
        public string? PaymentIntentId { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal? ShippingPrice { get; set; }
    } 
    #endregion
}
