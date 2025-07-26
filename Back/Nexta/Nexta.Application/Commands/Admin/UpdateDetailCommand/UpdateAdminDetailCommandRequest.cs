using Nexta.Application.DTO.RequestModels;
using MediatR;

namespace Nexta.Application.Commands.Admin.UpdateDetailCommand
{
    public class UpdateAdminDetailCommandRequest : IRequest<UpdateAdminDetailCommandResponse>
    {
		public Guid Id { get; init; }
		public string Name { get; init; } = null!;
		public string Article { get; init; } = null!;
		public string Description { get; init; } = null!;
		public int Status { get; init; }

		//public string OrderDate { get; init; } = string.Empty;
		//public string DeliveryDate { get; init; } = string.Empty;

		public int Count { get; init; }
		public int NewPrice { get; init; }
		public int OldPrice { get; init; }

		public bool IsVisible { get; init; }

		public Guid? ImageId { get; init; }
		public DetailImageRequest? Image { get; init; }
	}
}