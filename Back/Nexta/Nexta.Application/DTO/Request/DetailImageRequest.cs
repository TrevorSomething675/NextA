namespace Nexta.Application.DTO.Request
{
    public class DetailImageRequest
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Base64String { get; set; }
    }
}