using System;

namespace ExplicitStatus.Internals
{
    public class StatusDefinitionChainBuilder<T, TStatus> : IStatusDefinitionChainBuilder<T, TStatus>
    {
        private readonly StatusDefinitionBuilder<T, TStatus> statusDefinitionBuilder;
        private readonly StatusBuilder<T, TStatus> statusBuilder;

        internal StatusDefinitionChainBuilder(StatusDefinitionBuilder<T, TStatus> statusDefinitionBuilder, StatusBuilder<T, TStatus> statusBuilder)
        {
            this.statusDefinitionBuilder = statusDefinitionBuilder;
            this.statusBuilder = statusBuilder;
        }

        public IStatusDefinitionChainBuilder<T, TStatus> And<TProp>(System.Linq.Expressions.Expression<System.Func<T, TProp>> propertySelector, TProp value)
        {
            this.statusDefinitionBuilder.When(propertySelector, value);

            return this;
        }

        public IStatus<T, TStatus> Build()
        {
            this.statusBuilder.Add(this.statusDefinitionBuilder.Status, this.statusDefinitionBuilder);

            return this.statusBuilder.Build();
        }

        public IStatusDefinitionBuilder<T, TStatus> IsStatus(TStatus status)
        {
            this.statusBuilder.Add(this.statusDefinitionBuilder.Status, this.statusDefinitionBuilder);

            return this.statusBuilder.IsStatus(status);   
        }
    }
}
