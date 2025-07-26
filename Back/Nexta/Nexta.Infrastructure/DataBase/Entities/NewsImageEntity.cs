namespace Nexta.Infrastructure.DataBase.Entities
{
    public class NewsImageEntity : BaseEntity
    {
        public NewsEntity News { get; set; }
        public string Base64String { get; set; }
    }
}
