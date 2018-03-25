using System;
using System.Linq.Expressions;

namespace MarkUnit.Assemblies
{
    public interface IAssemblyCollection
    {
        IReducedAssemblyCollection HasName(Expression<Predicate<string>> nameFilter);
        IReducedAssemblyCollection HasNameMatching(string pattern);
    }
}
