using Nexta.Domain.Base;

namespace Nexta.Domain.Models.Basket
{
    public class Basket : IAggregateRoot
    {
        private readonly List<BasketProduct> _products = new List<BasketProduct>();

        public Guid UserId { get; private set; }

        public void AddProduct(Guid productId, int count)
        {
            var basketProduct = new BasketProduct(productId, count);
            _products.Add(basketProduct);
        }

        public IReadOnlyCollection<BasketProduct> Products => _products.AsReadOnly();
    }
}