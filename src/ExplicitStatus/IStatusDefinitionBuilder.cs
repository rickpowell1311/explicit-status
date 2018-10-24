using System;
using System.Linq.Expressions;

namespace ExplicitStatus
{
    public interface IStatusDefinitionBuilder<T, TStatus>
    {
        IStatusDefinitionChainBuilder<T, TStatus> When<TProp>(Expression<Func<T, TProp>> propertySelector, TProp value);
    }
}
