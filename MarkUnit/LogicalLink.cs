namespace MarkUnit
{
    internal class LogicalLink<T> : IRule<T>
    {
        public LogicalLink(T followUp)
        {
            FollowUp = followUp;
        }

        internal T FollowUp { get; }

        public T And()
        {
            PredicateString.Add("and");
            return FollowUp;
        }
    }
}
