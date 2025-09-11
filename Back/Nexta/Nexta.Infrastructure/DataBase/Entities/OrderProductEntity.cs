namespace Nexta.Infrastructure.DataBase.Entities
{
    public class OrderProductEntity
    {
        public int Count { get; set; }
        public Guid OrderId { get; set; }
        public OrderEntity Order { get; set; } = null!;

        public Guid ProductId { get; set; }
        public ProductEntity Product { get; set; } = null!;
    }
}
