using Nexta.Application.Commands.Categories.AddCategoryCommand;
using Nexta.Web.Models.Categories;
using AutoMapper;

namespace Nexta.Web.Mappings.Admin
{
    public class AdminCategoryProfile : Profile
    {
        public AdminCategoryProfile()
        {
            CreateMap<AddCategoryRequest, AddCategoryCommand>();
        }
    }
}
