using System;

namespace ExplicitStatus.Internals
{
    public class StatusDefinitionBuilder<T, TStatus> : IStatusDefinitionBuilder<T, TStatus>
    {
        internal StatusDefinitionBuilder()
        {
        }

        public IStatusBuilder<T, TStatus> When(Func<T, bool> condition)
        {
            throw new NotImplementedException();
        }
    }
}
