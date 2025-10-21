using MediatR;
using Nexta.Domain.Models.News;

namespace Nexta.Application.Commands.Admin.AddNewsCommand
{
    public class AddNewsCommand : IRequest<News>
    {
		public string? Header { get; set; }
		public string? Description { get; set; }
    }
}