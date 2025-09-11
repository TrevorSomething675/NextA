using Nexta.Application.DTO.Request;
using MediatR;

namespace Nexta.Application.Commands.Admin.AddNewsCommand
{
    public class AddNewsCommand : IRequest<AddNewsCommandResponse>
    {
		public string? Header { get; set; }
		public string? Description { get; set; }
        public NewsImageRequest? Image { get; init; }
    }
}