using Nexta.Application.DTO.Category;
using MediatR;

namespace Nexta.Application.Queries.Categories.GetCategoriesQuery
{
    public class GetCategoriesQuery : IRequest<List<CategoryDto>> { }
}
