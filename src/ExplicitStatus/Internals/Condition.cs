using System;

namespace ExplicitStatus.Internals
{
    public class Condition
    {
        private readonly Func<object, object> selector;
        private readonly object value;

        internal Condition(Func<object, object> selector, object value)
        {
            this.selector = selector;
            this.value = value;
        }

        public bool IsTrue(object obj)
        {
            var property = selector(obj);

            if (property == null && this.value == null)
            {
                return true;
            }

            return property.Equals(value);
        }
    }
}
