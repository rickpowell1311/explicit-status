using System;

namespace ExplicitStatus
{
    public class InvalidStatusConfigurationException<T, TStatus> : InvalidOperationException
    {
        public InvalidStatusConfigurationException(TStatus status, string memberId) : base(GenerateMessage(status, memberId))
        {
        }

        private static string GenerateMessage(TStatus status, string memberId)
        {
            return $"Status '{status.ToString()}' defined for type '{typeof(T).Name}' does not include member '{memberId}'. Either ignore it in the builder configuration or include this member in the status definition";
        }
    }
}
