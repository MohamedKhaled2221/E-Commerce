using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.OrderModule;

namespace Services.Specifications
{
    internal class OrderWithPaymentIntentSpecifications : BaseSpecifications<Order, Guid>
    {
        public OrderWithPaymentIntentSpecifications(string paymentIntentid) : base(o => o.PaymentIntentId == paymentIntentid)
        {
            
        }
    }
}
