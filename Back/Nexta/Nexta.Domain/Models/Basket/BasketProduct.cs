using Nexta.Domain.Base;

namespace Nexta.Domain.Models.Basket
{
    public class BasketProduct : Entity
    {
        public Guid ProductId { get; private set; }
        public int Count { get; private set; }

        internal BasketProduct(Guid productId, int count)
        {
            if(productId == default)
                throw new ArgumentNullException("ProductId is required", nameof(productId));
            if (count == default)
                throw new ArgumentNullException("Count is required", nameof(count));

            Count = count;
            ProductId = productId;
        }
    }
}