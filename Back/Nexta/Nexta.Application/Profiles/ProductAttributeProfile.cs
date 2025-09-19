using Nexta.Application.DTO.Request;
using Nexta.Application.DTO.Response;
using Nexta.Domain.Models;
using AutoMapper;

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