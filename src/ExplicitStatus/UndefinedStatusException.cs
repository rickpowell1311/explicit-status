using System;

namespace ExplicitStatus
{
    public class UndefinedStatusException<T, TStatus> : InvalidOperationException
    {

        public UndefinedStatusException()
            : base($"No status of type '{typeof(TStatus).Name}' could be determined for type '{typeof(T).Name}'")
        {
        }

        public UndefinedStatusException(string memberId) : base(GenerateMessage(memberId))
        {
        }

        private static string GenerateMessage(string memberId)
        {
            return $"Status '{typeof(T).Name}' defined for type '{typeof(TStatus).Name}' does not include member '{memberId}'. Either ignore it in the builder configuration or include this member in the status definition";
        }
    }
}
