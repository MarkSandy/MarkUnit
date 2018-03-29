using System;
using System.Linq.Expressions;
using MarkUnit.Classes;

namespace MarkUnit.Assemblies
{
    public interface IAssemblyCollection : ICondition<IAssemblyCollection>
    {
        IReducedAssemblyCollection HasName(Expression<Predicate<string>> nameFilter);
        IReducedAssemblyCollection HasNameMatching(string pattern);
    }
}
