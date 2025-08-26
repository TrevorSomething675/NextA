using Nexta.Application.DTO.Response;

namespace Nexta.Application.Commands.Admin.UpdateDetailCommand
{
    public class UpdateAdminProductCommandResponse(ProductResponse product)
    {
        public ProductResponse Product { get; init; } = product;
    }
}
