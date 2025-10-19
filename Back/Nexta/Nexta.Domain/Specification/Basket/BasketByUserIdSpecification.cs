using Nexta.Domain.Models.Basket;

namespace Nexta.Domain.Specification
{
    public class BasketByUserIdSpecification : BaseSpecification<Basket>
    {
        public BasketByUserIdSpecification(Guid userId)
        {
            Creteria = b => b.Id == userId;
            AddInclude(b => b.Products);
        }
    }
}