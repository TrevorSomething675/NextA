namespace Nexta.Infrastructure.DataBase.Entities
{
    public class DetailImageEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Base64String { get; set; }
        public DetailEntity Detail { get; set; }
    }
}
