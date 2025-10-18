using Nexta.Application.Commands.Categories.AddCategoryCommand;
using Nexta.Application.DTO.Response;
using AutoMapper;
using Nexta.Domain.Models.Product;

namespace Nexta.Application.Profiles
{
    public class ProductCategoryProfile : Profile
    {
        public ProductCategoryProfile()
        {
            CreateMap<Category, ProductCategoryResponse>();
            CreateMap<AddCategoryCommand, Category>();
        }
    }
}