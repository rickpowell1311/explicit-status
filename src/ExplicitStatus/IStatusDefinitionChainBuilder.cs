using System;
using System.Linq.Expressions;

namespace ExplicitStatus
{
    public interface IStatusDefinitionChainBuilder<T, TStatus> : IStatusBuilder<T, TStatus>
    {
        IStatusDefinitionChainBuilder<T, TStatus> And<TProp>(Expression<Func<T, TProp>> propertySelector, TProp value);
    }
}
