using Nexta.Application.Commands.News;
using MediatR;

namespace Nexta.Application.Commands.Admin.AddNewsCommand
{
    public class AddNewsCommand : IRequest<NewsDto>
    {
		public string? Header { get; set; }
		public string? Description { get; set; }
    }
}