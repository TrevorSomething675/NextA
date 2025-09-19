using Nexta.Infrastructure.DataBase.Entities;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Infrastructure.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<ProductCategory, ProductCategoryEntity>().ReverseMap();
        }
    }
}