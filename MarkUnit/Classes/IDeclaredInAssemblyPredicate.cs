using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MarkUnit.Classes
{
    public interface IDeclaredInAssemblyPredicate<out TPostCondition>
    {
        TPostCondition IsDeclaredInAssembly(Assembly assembly);
        TPostCondition IsDeclaredInAssembly(string name);
        TPostCondition IsDeclaredInAssembly(Expression<Predicate<Assembly>> predicate);
        TPostCondition IsDeclaredInAssemblyMatching(string pattern);
    }
}
