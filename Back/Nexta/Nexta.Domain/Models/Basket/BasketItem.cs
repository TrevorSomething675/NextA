using Nexta.Domain.Base;

namespace Nexta.Domain.Models.Basket
{
    public class BasketItem : Entity
    {
        internal Guid BasketId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Count { get; private set; }

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
        }

        public void ChangeCount(int count)
        {
            Count = count;
        }
    }
}