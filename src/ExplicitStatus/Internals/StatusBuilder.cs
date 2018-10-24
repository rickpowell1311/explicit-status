using System;

namespace ExplicitStatus.Internals
{
    public class StatusBuilder<TStatus> : IStatusBuilder<TStatus>
    {
        internal StatusBuilder()
        {
        }

        public IStatusBuilder<T, TStatus> FromType<T>(Action<IStatusBuilderConfiguration<T, TStatus>> config)
        {
            throw new NotImplementedException();
        }
    }

    public class StatusBuilder<T, TStatus> : IStatusBuilder<T, TStatus>
    {
        internal StatusBuilder()
        {
        }

        public IStatusDefinitionBuilder<T, TStatus> IsStatus(TStatus status)
        {
            throw new NotImplementedException();
        }
    }
}
