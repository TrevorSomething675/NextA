using Nexta.Application.DTO.Admin;

namespace Nexta.Application.Queries.Admin.GetProductQuery
{
    public class GetAdminProductQueryResponse(AdminProductResponse product)
    {
        public AdminProductResponse Product { get; init; } = product;
    }
}