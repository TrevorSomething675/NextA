using Nexta.Domain.Specification.Abstractions;
using System.Linq.Expressions;

namespace Nexta.Domain.Specification
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Creteria { get; protected set; } = (T => true);
        public List<string> Includes { get; protected set; } = new();
        public Expression<Func<T, object>>? OrderBy { get; protected set; }
        public Expression<Func<T, object>>? OrderByDescending { get; protected set; }
        public string SearchTerm { get; protected set; } = string.Empty;

        public int PageNumber { get; protected set; } = 1;
        public int PageSize { get; protected set; } = 8;

        protected void AddInclude(params string[] include)
            => Includes.AddRange(include);

        protected void ApplyOrderBy(Expression<Func<T, object>> orderBy)
            => OrderBy = orderBy;

        protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescending)
            => OrderByDescending = orderByDescending;
    }
}