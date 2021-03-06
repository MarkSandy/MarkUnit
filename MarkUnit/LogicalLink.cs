﻿namespace MarkUnit
{
    internal class LogicalLink<T> : IRule<T>, IFollowUp<T>
    {
        public LogicalLink(T followUp)
        {
            FollowUp = followUp;
        }

        public T FollowUp { get; }

        public T And()
        {
            PredicateString.Add("and");
            return FollowUp;
        }
    }

    public interface IFollowUp<T>
    {
        T FollowUp { get; }
    }
}
