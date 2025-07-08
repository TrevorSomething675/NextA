namespace Nexta.Application.DTO.RequestModels
{
    public class DetailRequest
    {
		public Guid Id { get; init; }
		public string Name { get; init; } = null!;
		public string Article { get; init; } = null!;
		public string Description { get; init; } = null!;
	}
}