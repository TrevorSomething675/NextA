using Nexta.Domain.Enums;
using Nexta.Domain.Base;

namespace Nexta.Domain.Models.Order
{
    public class Order : Entity
    {
        private readonly List<OrderItem> _products = new List<OrderItem>();

        public Guid UserId { get; }
        public OrderStatus Status { get; private set; }
        public DateTimeOffset CreatedDate { get; private set; }

        private Order() { }

        public Order(Guid userId, OrderStatus status)
        {
            UserId = userId;
            Status = status;
            CreatedDate = DateTimeOffset.Now;
        }

        public void UpdateProduct(Guid productId, int count)
        {
            var product = _products.Find(p => p.ProductId == productId);

            if (product == null)
                throw new ArgumentNullException($"Product with id [{productId}] is null");

            product.ChangeCount(count);
        }

        public void UpdateStatus(OrderStatus status)
        {
            Status = status;
        }

        public void ReplaceProducts(List<OrderItem> products)
        {
            _products.Clear();
            _products.AddRange(products);
        }

        public void DeleteProduct(Guid productId)
        {
            var orderProduct = _products.FirstOrDefault(x => x.ProductId == productId);
            _products.Remove(orderProduct);
        }

        public void AddProduct(Guid productId, int count = 1)
        {
            var orderProduct = new OrderItem(Id, productId, count);
            _products.Add(orderProduct);
        }

        public IReadOnlyCollection<OrderItem> Products => _products.AsReadOnly();
    }
}