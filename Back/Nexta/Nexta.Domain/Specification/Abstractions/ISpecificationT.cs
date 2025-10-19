using System.Linq.Expressions;

namespace Nexta.Domain.Specification.Abstractions
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Creteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        Expression<Func<T, object>>? OrderBy { get; }
        Expression<Func<T, object>>? OrderByDescending { get; }

        int PageNumber { get; }
        int PageSize { get; }
    }
}