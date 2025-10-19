namespace Nexta.Application.DTO.Product
{
    public class ProductImageDto
    {
        public Guid ProductId { get; init; }
        public string Name { get; init; }
        public string Base64String { get; init; }
    }
}