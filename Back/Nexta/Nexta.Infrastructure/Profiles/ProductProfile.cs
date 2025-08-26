using Nexta.Infrastructure.DataBase.Entities;
using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Models.Images;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Infrastructure.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductEntity>().ReverseMap();
            CreateMap<PagedData<Product>, PagedData<ProductEntity>>().ReverseMap();

            CreateMap<ProductImage, ProductImageEntity>().ReverseMap();
        }
    }
}