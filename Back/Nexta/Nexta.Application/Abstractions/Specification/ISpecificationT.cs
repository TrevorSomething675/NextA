using System.Linq.Expressions;

namespace Nexta.Application.Abstractions.Specification
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Creteria { get; }
        List<string> Includes { get; }
        Expression<Func<T, object>>? OrderBy { get; }
        Expression<Func<T, object>>? OrderByDescending { get; }
        public string SearchTerm { get; }

        int PageNumber { get; }
        int PageSize { get; }
    }
}