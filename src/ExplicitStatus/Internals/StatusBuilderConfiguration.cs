using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExplicitStatus.Internals
{
    public class StatusBuilderConfiguration<T, TStatus> : IStatusBuilderConfiguration<T, TStatus>
    {
        private readonly List<string> ignored;

        public IEnumerable<string> Ignored => this.ignored;

        internal StatusBuilderConfiguration()
        {
            this.ignored = new List<string>();    
        }

        public void Ignore<TProp>(Expression<Func<T, TProp>> ignore)
        {
            if (!(ignore.Body is MemberExpression memberExpression))
            {
                throw new InvalidOperationException($"Cannot ignore member of type '{typeof(TProp).Name}' in type '{typeof(T).Name}'");
            }

            var memberId = memberExpression.Member.Name;
            ignored.Add(memberId);
        }
    }
}
