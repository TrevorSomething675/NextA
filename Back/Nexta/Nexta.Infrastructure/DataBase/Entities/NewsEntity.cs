namespace Nexta.Infrastructure.DataBase.Entities
{
    public class NewsEntity : BaseEntity
    {
		public string Header { get; set; }
		public string Description { get; set; }

		public Guid ImageId { get; set; }
		public ImageEntity Image { get; set; }
	}
}