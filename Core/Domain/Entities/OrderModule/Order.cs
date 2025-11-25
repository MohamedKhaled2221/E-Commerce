global using ShippingAddress = Domain.Entities.OrderModule.Address;
namespace Domain.Entities.OrderModule
{
    #region Part 1 Order Module Entities
    public class Order : BaseEntity<Guid>
    {
        public Order()
        {

        }
        public Order(string userEmail, 
            ShippingAddress shippingAddress,
            ICollection<OrderItem> orderItems,
            DeliveryMethod deliveryMethod, decimal subtotal, string paymentIntentId)
        {
            UserEmail = userEmail;
            ShippingAddress = shippingAddress;
            OrderItems = orderItems;
            DeliveryMethod = deliveryMethod;
            Subtotal = subtotal;
            PaymentIntentId = paymentIntentId;
        }

        public string UserEmail { get; set; } = string.Empty;
        public Address ShippingAddress { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }


        public OderPaymentStatus PaymentStatus { get; set; } = OderPaymentStatus.Pending;
        public DeliveryMethod DeliveryMethod { get; set; }
        public int? DeliveryMethodId { get; set; }

        // OrderItem.Price * OrderItem.Quantity
        // Total == Subtotal + DeliveryMethod.Price [ Derieved Atribute ] --> DTO OR Mapping Profile
        public decimal Subtotal { get; set; }
        public string PaymentIntentId { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;




    } 
    #endregion
}
