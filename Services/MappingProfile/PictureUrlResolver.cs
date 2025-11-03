using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Execution;
using AutoMapper.Internal;
using Domain.Entities.ProdutModule;
using Microsoft.Extensions.Configuration;
using Shared.Dtos.ProductModule;

namespace Services.MappingProfile
{
    internal class PictureUrlResolver(IConfiguration configuration) : IValueResolver<Product, ProductResultDto, string>
    {
        #region Part 5 Picture Url Resolver
        public string Resolve(Product source, ProductResultDto destination, string destMember, ResolutionContext context)
        {
            // https://localhost:7006 +  images/products/ItalianChickenMarinade.png
            if (string.IsNullOrWhiteSpace(source.PictureUrl))
            {
                return string.Empty;
            }
            // BaseUrl + PictureUrl
            // https://localhost:7006 +  images/products/ItalianChickenMarinade.png

            return $"{configuration["BaseUrl"]}{source.PictureUrl}";

        } 
        #endregion
    }
}
