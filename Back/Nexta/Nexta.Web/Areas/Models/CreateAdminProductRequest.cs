namespace Nexta.Web.Areas.Models
{
    public class CreateAdminProductRequest
    {
        public string Name { get; set; } = null!;
        public string Article { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Status { get; set; }

        public string? OrderDate { get; set; }
        public string? DeliveryDate { get; set; }

        public int Count { get; set; }
        public int NewPrice { get; set; }
        public int? OldPrice { get; set; }

        public bool IsVisible { get; set; }

        public string? ImageName { get; init; }
        public string? ImageBase64String { get; init; }
    }
}