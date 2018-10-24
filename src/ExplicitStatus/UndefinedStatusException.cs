using System;

namespace ExplicitStatus
{
    public class UndefinedStatusException<T, TStatus> : InvalidOperationException
    {
        public UndefinedStatusException()
            : base($"No status of type '{typeof(TStatus).Name}' could be determined for type '{typeof(T).Name}'")
        {
        }
    }
}
