namespace Nexta.Application.DTO
{
    public class OrderDetailResponse
    {
        public Guid DetailId { get; init; }
        public DetailResponse Detail { get; init; }

        public int Count { get; set; }
    }
}