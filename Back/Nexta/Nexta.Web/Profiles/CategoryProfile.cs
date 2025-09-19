using Nexta.Application.Commands.Categories.DeleteCategoryCommand;
using Nexta.Application.Commands.Categories.AddCategoryCommand;
using Nexta.Web.Models.Categories;
using AutoMapper;

namespace Nexta.Web.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<AddCategoryRequest, AddCategoryCommand>()
                .ForMember(src => src.Name, opt => opt.MapFrom(x => x.Name ?? string.Empty));

            CreateMap<DeleteCategoryRequest, DeleteCategoryCommand>();
        }
    }
}