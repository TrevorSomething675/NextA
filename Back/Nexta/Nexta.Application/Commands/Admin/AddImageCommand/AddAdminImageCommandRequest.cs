using MediatR;

namespace Nexta.Application.Commands.Admin.AddImageCommand
{
    public class AddAdminImageCommandRequest : IRequest<AddAdminImageCommandResponse>
    {
		public string Name { get; set; } = null!;
		public string Bucket { get; set; } = null!;
		public string? Base64String { get; set; }
	}
}