using Nexta.Domain.Base;

namespace Nexta.Domain.Models.Order
{
    public class OrderProduct : Entity
    {
        public int Count { get; private set; }
        public Guid ProductId { get; private set; }

        public OrderProduct(Guid productId, int count)
        {
            Count = count;
            ProductId = productId;
        }
    }
}