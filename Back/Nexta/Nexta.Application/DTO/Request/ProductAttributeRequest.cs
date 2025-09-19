namespace Nexta.Application.DTO.Request
{
    public class ProductAttributeRequest
    {
        public Guid ProductId { get; set; }
        public ProductRequest? Product { get; set; }

        public string Key { get; set; }
        public string Value { get; set; }
    }
}