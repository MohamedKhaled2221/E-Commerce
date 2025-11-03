using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities.ProdutModule;
using Shared.Dtos.ProductModule;

namespace Services.MappingProfile
{
    public class ProductProfile :Profile
    {
        public ProductProfile()
        {
         
            CreateMap<Product, ProductResultDto>()
                      .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.ProductBrand.Name))
                       .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.ProductType.Name))
                       .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<PictureUrlResolver>());
            CreateMap<ProductType, TypeResultDto>();
            CreateMap<ProductBrand, BrandResultDto>();
        }
    }
}
