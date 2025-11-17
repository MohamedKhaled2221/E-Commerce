using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.OrderModule;

namespace Services.Specifications
{
    internal class OrderWithIncludeSpecifications : BaseSpecifications<Order, Guid>
    {
        // Get Order by Id --> Criteria --> Id => o.Id == Id & Include --> DeliveryMethod , OrderItems
        public OrderWithIncludeSpecifications(Guid id )
            : base(o => o.Id ==id)
        {
            AddIncludes(o => o.DeliveryMethod);
            AddIncludes(o => o.OrderItems);
        }
        // Get All Orders For User by Email 
        public OrderWithIncludeSpecifications(string email)
           : base(o => o.UserEmail == email)
        {
            AddIncludes(o => o.DeliveryMethod);
            AddIncludes(o => o.OrderItems);

            SetOrderBy(o => o.OrderDate);
        }
    }
}
