using System;
using System.Linq.Expressions;

namespace MarkUnit
{
    public interface INamePredicate<out TPostCondition>
    {
        TPostCondition HasName(Expression<Predicate<string>> nameFilter);
        TPostCondition HasNameMatching(string pattern);
    }
}