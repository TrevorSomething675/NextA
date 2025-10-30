using Nexta.Domain.Base;

namespace Nexta.Domain.Models.Baskets
{
    public class BasketItem : Entity
    {
        public Guid BasketId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Count { get; private set; }

        private BasketItem() { }

        internal BasketItem(Guid basketId, Guid productId, int count)
        {
            if (basketId == default)
                throw new ArgumentNullException("BasketId is required", nameof(basketId));
            if (productId == default)
                throw new ArgumentNullException("ProductId is required", nameof(productId));
            if (count == default)
                throw new ArgumentNullException("Count is required", nameof(count));

            Count = count;
            ProductId = productId;
            BasketId = basketId;
        }

        public void ChangeCount(int count)
        {
            Count = count;
        }
    }
}