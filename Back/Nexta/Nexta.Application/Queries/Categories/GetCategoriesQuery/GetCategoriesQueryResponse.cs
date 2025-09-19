using Nexta.Application.DTO.Response;

namespace Nexta.Application.Queries.Categories.GetCategoriesQuery
{
    public class GetCategoriesQueryResponse(List<ProductCategoryResponse> categories)
    {
        public List<ProductCategoryResponse> Categories { get; set; } = categories;
    }
}