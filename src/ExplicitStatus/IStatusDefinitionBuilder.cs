using System;

namespace ExplicitStatus
{
    public interface IStatusDefinitionBuilder<T, TStatus>
    {
        IStatusBuilder<T, TStatus> When(Func<T, bool> condition);
    }
}
