using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExplicitStatus.Internals
{
    public class StatusBuilderConfiguration<T, TStatus> : IStatusBuilderConfiguration<T, TStatus>
    {
        private readonly List<Type> ignored;

        internal StatusBuilderConfiguration()
        {
            this.ignored = new List<Type>();    
        }

        public void Ignore<TProp>(Expression<Func<T, TProp>> ignore)
        {
            ignored.Add(typeof(TProp));
        }
    }
}
