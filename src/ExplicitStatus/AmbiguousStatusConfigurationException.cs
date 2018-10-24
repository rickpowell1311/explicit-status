using System;

namespace ExplicitStatus
{
    public class AmbiguousStatusConfigurationException<TStatus> : InvalidOperationException
    {
        public AmbiguousStatusConfigurationException(TStatus ambiguousStatus) 
            : base(GenerateMessage(ambiguousStatus))
        {
        }

        private static string GenerateMessage(TStatus ambiguousStatus)
        {
            return $"More than one status definition supplied for status '{ambiguousStatus.ToString()}'";
        }
    }
}
