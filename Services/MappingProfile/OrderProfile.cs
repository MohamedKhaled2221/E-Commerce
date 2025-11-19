global using ShippingAddress = Domain.Entities.OrderModule.Address;
using AutoMapper;
using Domain.Entities.IdentityModule;
using Domain.Entities.OrderModule;
using Shared.Dtos.OrderModule;

namespace Services.MappingProfile
{
    #region Part 4 Order Profile { Mapping Profile } 
    internal class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<ShippingAddress, AddressDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.ProductId))
             .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
             .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.Product.PictureUrl))
              .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<OrderItemPictureUrlResolver>());
            CreateMap<Order, OrderResult>()
                  .ForMember(d => d.PaymentStatus, o => o.MapFrom(s => s.PaymentStatus))
                  .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                    .ForMember(d => d.Total, o => o.MapFrom(s => s.Subtotal + s.DeliveryMethod.Price));

            CreateMap<DeliveryMethod, DeliveryMethodResult>();
        }

    } 
    #endregion
}
