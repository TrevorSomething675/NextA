namespace Nexta.Domain.Models
{
    public class News
    {
        public Guid Id { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }

        public Guid ImageId { get; set; }
        public Image Image { get; set; }
    }
}