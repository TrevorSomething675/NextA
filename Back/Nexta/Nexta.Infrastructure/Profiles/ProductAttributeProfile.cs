using Nexta.Infrastructure.DataBase.Entities;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Infrastructure.Profiles
{
    public class ProductAttributeProfile : Profile
    {
        public ProductAttributeProfile()
        {
            CreateMap<ProductAttribute, ProductAttributeEntity>().ReverseMap();
        }
    }
}