using Nexta.Domain.Models.Baskets;

namespace Nexta.Domain.Specification
{
    public class BasketByUserIdSpecification : BaseSpecification<Basket>
    {
        public BasketByUserIdSpecification(Guid userId)
        {
            Creteria = b => b.UserId == userId;
            AddInclude("Products");
        }
    }
}