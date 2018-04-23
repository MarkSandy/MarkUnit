using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace MarkUnit.Assemblies
{
    internal class AssemblyRule
        : RuleBase<IAssembly, IAssemblyTestCondition, IAssemblyRule>,
          IInternalAssemblyTestCondition
    {
        public AssemblyRule(IAssertionVerifier<IAssembly> verifier) : base(verifier)
        {
            LogicalLink = new AssemblyLogicalLink(this);
        }

        public AssemblyRule(IFilter<IAssembly> items, bool negateAssertion)
            : base(items, negateAssertion)
        {
            LogicalLink = new AssemblyLogicalLink(this);
        }
        
        public IAssemblyRule ReferenceAssembly(string name)
        {
            PredicateString.Add($"reference assembly '{name}'");
            return AppendCondition(r => r.ReferencedAssemblies.Any(x=>x.Name == name));
        }

        public IAssemblyRule HaveName(Expression<Predicate<string>> nameFilterExpression)
        {
            PredicateString.Add($"have name matching {nameFilterExpression}");
            var nameFilter = nameFilterExpression.Compile();
            return AppendCondition(r => nameFilter(r.Name));
        }

        public IAssemblyRule ReferenceAssembliesMatching(string pattern)
        {
            PredicateString.Add($"reference assemblies matching '{pattern}'");
            return AppendCondition(r => r.ReferencedAssemblies.Any(x=>x.Name.Matches(pattern)));
        }

        public IAssemblyRule ReferenceAssembliesMatching(Expression<Predicate<IAssembly>> assemblyFilterExpression)
        {
            PredicateString.Add($"reference assemblies matching '{assemblyFilterExpression}'");
            var assemblyFilter = assemblyFilterExpression.Compile();
            return AppendCondition(a => a.ReferencedAssemblies.Any(r => assemblyFilter(r)));
        }
    }
}
