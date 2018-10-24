using System;
using System.Linq;

namespace ExplicitStatus
{
    public class AmbiguousStatusException<T, TStatus> : InvalidOperationException
    {
        public AmbiguousStatusException(params TStatus[] ambiguousStatuses) : 
            base(GenerateMessage(ambiguousStatuses))
        {
        }

        private static string GenerateMessage(params TStatus[] ambiguousStatuses)
        {
            return $"The current status of type '{typeof(T).Name}' was found to have multiple statuses: {ListedStatuses(ambiguousStatuses)}";
        }

        private static string ListedStatuses(params TStatus[] ambiguousStatuses)
        {
            var listed = "either ";

            if (ambiguousStatuses.Length < 2)
            {
                throw new InvalidOperationException("Cannot have an ambiguous status when less than 2 statuses match");
            }

            for (int i = 0; i < ambiguousStatuses.Length; i++)
            {
                listed += $"'{ambiguousStatuses[0].ToString()}'";

                if (i == ambiguousStatuses.Length - 2)
                {
                    listed += " or ";
                }
                else
                {
                    listed += ", ";
                }
            }

            return listed;
        }
    }
}
