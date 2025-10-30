using Nexta.Application.DTO.Categories;
using MediatR;

namespace Nexta.Application.Queries.Categories.GetCategoriesQuery
{
    public class GetCategoriesQuery : IRequest<List<CategoryDto>> { }
}
