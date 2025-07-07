using Nexta.Application.DTO;

namespace Nexta.Application.Commands.Admin.UpdateDetailCommand
{
    public class UpdateAdminDetailCommandResponse(DetailResponse detail)
    {
        public DetailResponse Detail { get; set; } = detail;
    }
}
