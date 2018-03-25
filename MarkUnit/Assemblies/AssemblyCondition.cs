using System;
using System.Linq.Expressions;

namespace MarkUnit.Assemblies
{
    internal class AssemblyCollection 
        : TestCollectionBase<IAssembly, IAssemblyCollection, IAssemblyTestCondition,IReducedAssemblyCollection>
        , IAssemblyCollection
    {
        public AssemblyCollection(FilteredAssemblies assemblies, bool negate) : base(assemblies)
        {
            FilterCondition = new AssemblyFilterCondition(this, assemblies, negate);
        }

        private IReducedAssemblyCollection InnerHasName(Predicate<string> nameFilter)
        {
            return AppendCondition(a => nameFilter(a.Name));
        }

        public IReducedAssemblyCollection HasName(Expression<Predicate<string>> nameFilter)
        {
            PredicateString.Add($"has name matching {nameFilter}");
            return InnerHasName(nameFilter.Compile());
        }

        public IReducedAssemblyCollection HasNameMatching(string pattern)
        {
            PredicateString.Add($"has name matching '{pattern}'");
            return InnerHasName(n => n.Matches(pattern));
        }
    }
}