using Nexta.Domain.Base;

namespace Nexta.Domain.Models.Basket
{
    public class Basket : Entity, IAggregateRoot
    {
        private readonly List<BasketItem> _products = new List<BasketItem>();

        public Guid UserId { get; private set; }

        public void AddProduct(Guid productId, int count)
        {
            var basketProduct = new BasketItem(Id, productId, count);
            _products.Add(basketProduct);
        }

        public void UpdateProduct(Guid productId, int count)
        {
            var product = _products.Find(p => p.ProductId == productId);

            if (product == null)
                throw new ArgumentNullException($"Product with id [{productId}] is null");

            product.ChangeCount(count);
        }

        public void Clear()
        {
            _products.Clear();
        }

        public void RemoveProduct(Guid productId)
        {
            var product = _products.Find(p => p.ProductId == productId);

            if (product == null)
                throw new ArgumentNullException($"Product with id [{productId}] is null");

            _products.Remove(product);
        }

        public IReadOnlyCollection<BasketItem> Products => _products.AsReadOnly();
    }
}