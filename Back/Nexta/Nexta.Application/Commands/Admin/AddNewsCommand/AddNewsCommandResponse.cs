using Nexta.Application.DTO;

namespace Nexta.Application.Commands.Admin.AddNewsCommand
{
    public class AddNewsCommandResponse(NewsResponse news)
    {
		public NewsResponse News { get; init; } = news;
	}
}