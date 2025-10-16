using Nexta.Application.DTO.Request;
using Nexta.Application.DTO.Response;
using AutoMapper;
using Nexta.Domain.Models.Product;

namespace Nexta.Application.Profiles
{
    public class ProductAttributeProfile : Profile
    {
        public ProductAttributeProfile()
        {
            CreateMap<ProductAttributeRequest, ProductAttribute>();
            CreateMap<ProductAttribute, ProductAttributeResponse>();
        }
    }
}