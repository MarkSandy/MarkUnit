using System;
using System.Linq;

namespace MarkUnit.Assemblies
{
    internal class AssemblyRule
        : RuleBase<IAssembly, IAssemblyTestCondition, IAssemblyRule>,
          IInternalAssemblyTestCondition
    {
        public AssemblyRule(IFilter<IAssembly> items, bool negateAssertion)
            : base(items, negateAssertion)
        {
            LogicalLink = new AssemblyLogicalLink(this);
        }

        public IAssemblyRule ReferenceAssembly(string name)
        {
            PredicateString.Add($"reference assembly '{name}'");
            return AppendCondition(r => r.Name == name);
        }

        public IAssemblyRule ReferenceAssembliesMatching(string pattern)
        {
            PredicateString.Add($"reference assemblies matching '{pattern}'");
            return AppendCondition(r => r.Name.Matches(pattern));
        }

        public IAssemblyRule ReferenceAssembliesMatching(Predicate<IAssembly> func)
        {
            PredicateString.Add($"reference assemblies matching '{func}'");
            return AppendCondition(a => a.ReferencedAssemblies.Any(r => func(r)));
        }
    }
}
