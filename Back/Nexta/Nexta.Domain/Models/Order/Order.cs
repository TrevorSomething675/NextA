using Nexta.Domain.Enums;
using Nexta.Domain.Base;

namespace Nexta.Domain.Models.Order
{
    public class Order : Entity
    {
        private readonly List<OrderProduct> _orderProducts = new List<OrderProduct>();

        public Guid UserId { get; }
        public OrderStatus Status { get; }
        public DateTimeOffset CreatedDate { get; }

        private Order() { }

        internal Order(Guid userId)
        {
            UserId = userId;
            Status = OrderStatus.Accepted;
            CreatedDate = DateTimeOffset.Now;
        }

        public void AddProduct(Guid productId, int count = 1)
        {
            var orderProduct = new OrderProduct(productId, count);
            _orderProducts.Add(orderProduct);
        }

        public IReadOnlyCollection<OrderProduct> OrderProducts => _orderProducts.AsReadOnly();
    }
}