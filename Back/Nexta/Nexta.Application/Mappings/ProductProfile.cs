using Nexta.Application.DTO.Products;
using Nexta.Domain.Models.Products;
using Nexta.Domain.Base;
using AutoMapper;

namespace Nexta.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<PagedData<Product>, PagedData<ProductDto>>();
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
