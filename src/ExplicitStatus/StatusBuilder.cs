using ExplicitStatus.Internals;
using System;

namespace ExplicitStatus
{
    public static class StatusBuilder
    {
        public static IStatusBuilder<TStatus> BuildStatus<TStatus>()
        {
            return new StatusBuilder<TStatus>();
        }
    }

    public interface IStatusBuilder<TStatus>
    {
        IStatusBuilder<T, TStatus> FromType<T>(Action<IStatusBuilderConfiguration<T, TStatus>> config = null);
    }

    public interface IStatusBuilder<T, TStatus>
    {
        IStatusDefinitionBuilder<T, TStatus> IsStatus(TStatus status);

        IStatus<T, TStatus> Build();
    }
}
