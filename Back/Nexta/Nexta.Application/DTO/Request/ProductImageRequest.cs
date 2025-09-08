namespace Nexta.Application.DTO.Request
{
    public class ProductImageRequest
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Base64String { get; set; }
    }
}