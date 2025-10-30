namespace Nexta.Application.DTO.Products
{
    public class ProductImageDto
    {
        public Guid ProductId { get; init; }
        public string Name { get; init; }
        public string Base64String { get; init; }
    }
}