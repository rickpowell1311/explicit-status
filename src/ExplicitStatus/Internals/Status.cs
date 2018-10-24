using System;

namespace ExplicitStatus.Internals
{
    public class Status<T, TStatus> : IStatus<T, TStatus>
    {
        private readonly StatusBuilder<T, TStatus> statusBuilder;

        internal Status(StatusBuilder<T, TStatus> statusBuilder)
        {
            this.statusBuilder = statusBuilder;
        }

        public TStatus GetFor(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
