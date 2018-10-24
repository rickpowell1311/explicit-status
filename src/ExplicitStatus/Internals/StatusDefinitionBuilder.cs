using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExplicitStatus.Internals
{
    public class StatusDefinitionBuilder<T, TStatus> : IStatusDefinitionBuilder<T, TStatus>
    {
        private readonly StatusBuilder<T, TStatus> statusBuilder;
        private readonly List<Condition> conditions;

        public TStatus Status { get; }

        public bool IsDefault { get; private set; }

        public IEnumerable<Condition> Conditions => this.conditions;

        internal StatusDefinitionBuilder(StatusBuilder<T, TStatus> statusBuilder, TStatus status)
        {
            this.statusBuilder = statusBuilder;
            this.Status = status;
            this.IsDefault = false;

            this.conditions = new List<Condition>();
        }

        public IStatusDefinitionChainBuilder<T, TStatus> When<TProp>(Expression<Func<T, TProp>> propertySelector, Func<TProp, bool> condition)
        {
            if (condition == null)
            {
                throw new ArgumentException($"Condition for status '{Status.ToString()}' from type '{typeof(T).Name}' cannot be null");
            }

            if (!(propertySelector.Body is MemberExpression memberExpression))
            {
                throw new InvalidOperationException($"Cannot define condition for member of type '{typeof(TProp).Name}' in type '{typeof(T).Name}'");
            }

            conditions.Add(new Condition(
                memberExpression.Member.Name,
                (object t) => condition(propertySelector.Compile()((T)t))));

            return new StatusDefinitionChainBuilder<T, TStatus>(this, this.statusBuilder);
        }

        public IStatusBuilder<T, TStatus> ByDefault()
        {
            IsDefault = true;

            this.statusBuilder.Add(Status, this);

            return this.statusBuilder;
        }
    }
}
