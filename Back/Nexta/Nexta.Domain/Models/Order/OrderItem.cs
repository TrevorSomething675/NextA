using Nexta.Domain.Base;

namespace Nexta.Domain.Models.Order
{
    public class OrderItem : Entity
    {
        public Guid OrderId { get; private set; }
        public int Count { get; private set; }
        public Guid ProductId { get; private set; }

        public OrderItem(Guid orderId, Guid productId, int count)
        {
            Count = count;
            OrderId = orderId;
            ProductId = productId;
        }

        public void ChangeCount(int count)
        {
            Count = count;
        }
    }
}