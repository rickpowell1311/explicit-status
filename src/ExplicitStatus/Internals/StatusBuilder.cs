using System;
using System.Collections.Generic;
using System.Linq;

namespace ExplicitStatus.Internals
{
    public class StatusBuilder<TStatus> : IStatusBuilder<TStatus>
    {
        internal StatusBuilder()
        {
        }

        public IStatusBuilder<T, TStatus> FromType<T>(Action<IStatusBuilderConfiguration<T, TStatus>> config = null)
        {
            return new StatusBuilder<T, TStatus>(config);
        }
    }

    public class StatusBuilder<T, TStatus> : IStatusBuilder<T, TStatus>
    {
        private readonly Dictionary<TStatus, StatusDefinitionBuilder<T, TStatus>> definitions;

        public Action<IStatusBuilderConfiguration<T, TStatus>> Config { get; }

        public IEnumerable<KeyValuePair<TStatus, StatusDefinitionBuilder<T, TStatus>>> Definitions => this.definitions;

        internal StatusBuilder(Action<IStatusBuilderConfiguration<T, TStatus>> config)
        {
            Config = config;
            this.definitions = new Dictionary<TStatus, StatusDefinitionBuilder<T, TStatus>>();
        }

        public IStatusDefinitionBuilder<T, TStatus> IsStatus(TStatus status)
        {
            if (this.definitions.Keys.Contains(status))
            {
                throw new AmbiguousStatusConfigurationException<TStatus>(status);
            }

            return new StatusDefinitionBuilder<T, TStatus>(this, status);
        }

        public void Add(TStatus status, StatusDefinitionBuilder<T, TStatus> definition)
        {
            this.definitions.Add(status, definition);
        }

        public IStatus<T, TStatus> Build()
        {
            return new Status<T, TStatus>(this); 
        }
    }
}
