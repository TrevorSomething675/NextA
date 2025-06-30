namespace Nexta.Infrastructure.DataBase.Entities
{
    public class OrderDetailEntity
    {
        public int Count { get; set; }
        public Guid OrderId { get; set; }
        public OrderEntity Order { get; set; } = null!;

		public Guid DetailId { get; set; }
        public DetailEntity Detail { get; set; } = null!;
    }
}