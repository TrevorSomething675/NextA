using Nexta.Application.Commands.Categories.AddCategoryCommand;
using Nexta.Application.DTO.Response;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Application.Profiles
{
    public class ProductCategoryProfile : Profile
    {
        public ProductCategoryProfile()
        {
            CreateMap<ProductCategory, ProductCategoryResponse>();
            CreateMap<AddCategoryCommand, ProductCategory>();
        }
    }
}