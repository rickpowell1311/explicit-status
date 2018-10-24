using System;

namespace ExplicitStatus.Internals
{
    public class Condition
    {
        private readonly string memberId;
        private readonly Func<object, bool> condition;

        public string MemberId => this.memberId;

        internal Condition(string memberId, Func<object, bool> condition)
        {
            this.memberId = memberId;
            this.condition = condition;
        }

        public bool IsTrue(object obj)
        {
            return condition(obj);
        }
    }
}
