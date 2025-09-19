namespace Nexta.Application.DTO.Response
{
    public class ProductAttributeResponse
    {
        public Guid ProductId { get; set; }
        public ProductResponse Product { get; set; }

        public string Key { get; set; }
        public string Value { get; set; }
    }
}
