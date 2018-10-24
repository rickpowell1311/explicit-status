using System;

namespace ExplicitStatus.Internals
{
    public class StatusDefinitionBuilder<T, TStatus> : IStatusDefinitionBuilder<T, TStatus>
    {
        public readonly TStatus status;
        private readonly StatusBuilder<T, TStatus> statusBuilder;

        public Func<T, bool> Condition { get; private set; }


        internal StatusDefinitionBuilder(StatusBuilder<T, TStatus> statusBuilder, TStatus status)
        {
            this.statusBuilder = statusBuilder;
            this.status = status;
        }

        public IStatusBuilder<T, TStatus> When(Func<T, bool> condition)
        {
            Condition = condition;

            this.statusBuilder.Add(this.status, this);

            return this.statusBuilder;
        }
    }
}
