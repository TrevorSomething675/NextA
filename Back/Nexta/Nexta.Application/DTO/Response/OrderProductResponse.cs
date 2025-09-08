namespace Nexta.Application.DTO.Response
{
    public class OrderProductResponse
    {
		public Guid Id { get; init; }
        public string Name { get; init; }
        public string Article { get; init; }
        public string Description { get; init; }
        public int NewPrice { get; init; }
        public int OldPrice { get; init; }
        public int Count { get; init; }
    }
}