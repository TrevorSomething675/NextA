using Nexta.Application.DTO.Admin;

namespace Nexta.Application.Commands.Admin.UpdateAdminProductCommand
{
    public class UpdateAdminProductCommandResponse(AdminProductResponse product)
    {
        public AdminProductResponse Product { get; init; } = product;
    }
}
