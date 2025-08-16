namespace Nexta.Infrastructure.DataBase.Entities
{
    public class NewsEntity : BaseEntity
    {
		public string? Header { get; set; }
		public string? Description { get; set; }

		public NewsImageEntity Image { get; set; }
	}
}