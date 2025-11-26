using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities.OrderModule;
using Microsoft.Extensions.Configuration;
using Shared.Dtos.OrderModule;

namespace Services.MappingProfile
{
    public class OrderItemPictureUrlResolver(IConfiguration configuration) : IValueResolver<OrderItem, OrderItemDto, string>
    {
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            // https://localhost:7006 +  images/products/ItalianChickenMarinade.png
            if (string.IsNullOrWhiteSpace(source.Product.PictureUrl))
            {
                return string.Empty;
            }
            // BaseUrl + PictureUrl
            // https://localhost:7006 +  images/products/ItalianChickenMarinade.png

            return $"{configuration["BaseUrl"]}{source.Product.PictureUrl}";
        }
    }
}
