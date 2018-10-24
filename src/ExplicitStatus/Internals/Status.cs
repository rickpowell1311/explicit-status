using System.Linq;
using System.Reflection;

namespace ExplicitStatus.Internals
{
    public class Status<T, TStatus> : IStatus<T, TStatus>
    {
        private readonly StatusBuilder<T, TStatus> statusBuilder;

        internal Status(StatusBuilder<T, TStatus> statusBuilder)
        {
            this.statusBuilder = statusBuilder;
        }

        public TStatus GetFor(T obj)
        {
            // Apply configuration
            var configuration = new StatusBuilderConfiguration<T, TStatus>();
            this.statusBuilder.Config?.Invoke(configuration);

            // Find matching status
            bool matchFound = false;
            var match = default(TStatus);

            bool defaultMatchFound = false;
            var defaultMatch = default(TStatus);

            foreach (var definitionBuilder in statusBuilder.Definitions)
            {
                var status = definitionBuilder.Key;
                var conditions = definitionBuilder.Value.Conditions;

                if (definitionBuilder.Value.IsDefault)
                {
                    defaultMatchFound = true;
                    defaultMatch = status;
                    continue;
                }

                bool conditionsMatch = true;
                foreach (var property in typeof(T).GetTypeInfo().DeclaredProperties)
                {
                    if (!configuration.Ignored.Contains(property.Name) && !conditions.Select(c => c.MemberId).Contains(property.Name))
                    {
                        throw new InvalidStatusConfigurationException<T, TStatus>(status, property.Name);
                    }

                    if (configuration.Ignored.Contains(property.Name))
                    {
                        continue;
                    }

                    var condition = conditions.Single(c => c.MemberId == property.Name);

                    if (!condition.IsTrue(obj))
                    {
                        conditionsMatch = false;
                        break;
                    }
                }

                if (conditionsMatch)
                {
                    if (matchFound)
                    {
                        throw new AmbiguousStatusException<T, TStatus>(match, status);
                    }

                    matchFound = true;
                    match = status;
                }
            }

            if (!matchFound)
            {
                if (defaultMatchFound)
                {
                    return defaultMatch;
                }

                throw new UndefinedStatusException<T, TStatus>();
            }

            return match;
        }
    }
}
