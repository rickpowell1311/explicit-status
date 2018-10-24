using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExplicitStatus.Internals
{
    public class StatusDefinitionBuilder<T, TStatus> : IStatusDefinitionBuilder<T, TStatus>
    {
        public TStatus Status { get; }
        private readonly StatusBuilder<T, TStatus> statusBuilder;
        private readonly List<Condition> conditions;

        internal StatusDefinitionBuilder(StatusBuilder<T, TStatus> statusBuilder, TStatus status)
        {
            this.statusBuilder = statusBuilder;
            this.Status = status;

            this.conditions = new List<Condition>();
        }

        public IStatusDefinitionChainBuilder<T, TStatus> When<TProp>(Expression<Func<T, TProp>> propertySelector, TProp value)
        {
            var condition = new Condition(
                (object t) => propertySelector.Compile()((T)t), value);

            conditions.Add(condition);

            return new StatusDefinitionChainBuilder<T, TStatus>(this, this.statusBuilder);
        }
    }
}
