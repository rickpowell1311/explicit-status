using System;
using System.Linq.Expressions;

namespace ExplicitStatus.Internals
{
    public class StatusBuilderConfiguration<T, TStatus> : IStatusBuilderConfiguration<T, TStatus>
    {
        internal StatusBuilderConfiguration()
        {
        }

        public void Ignore<TProp>(Expression<Func<T, TProp>> ignore)
        {
            throw new NotImplementedException();
        }
    }
}
