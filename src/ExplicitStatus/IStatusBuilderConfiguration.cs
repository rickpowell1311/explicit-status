using System;
using System.Linq.Expressions;

namespace ExplicitStatus
{
    public interface IStatusBuilderConfiguration<T, TStatus>
    {
        void Ignore<TProp>(Expression<Func<T, TProp>> ignore);
    }
}
