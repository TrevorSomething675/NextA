namespace Nexta.Web.Areas.Models
{
    public class AddNewsRequest
    {
        public string? Header { get; set; }
        public string? Description { get; set; }
        public string ImageName { get; set; }
        public string ImageBase64String { get; set; }
    }
}
