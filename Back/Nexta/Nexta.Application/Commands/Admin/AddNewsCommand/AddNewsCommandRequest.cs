using Nexta.Application.DTO.RequestModels;
using MediatR;

namespace Nexta.Application.Commands.Admin.AddNewsCommand
{
    public class AddNewsCommandRequest : IRequest<AddNewsCommandResponse>
    {
        public Guid Id { get; init; }
        public string? Header { get; init; }
        public string? Name { get; init; } = null!;

        public Guid ImageId { get; init; }
        public ImageRequest? Image { get; init; }
    }
}