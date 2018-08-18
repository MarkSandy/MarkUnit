using System;
using System.Linq;
using System.Linq.Expressions;

namespace MarkUnit.Assemblies
{
    internal class AssemblyRule
        : RuleBase<IAssembly, IAssemblyTestCondition, IAssemblyRule>,
          IInternalAssemblyTestCondition,
          IAssemblyRule
    {
        public AssemblyRule(IAssertionVerifier<IAssembly> verifier)
            : base(verifier) { }

        public AssemblyRule(IFilter<IAssembly> items, bool negateAssertion)
            : base(items, negateAssertion) { }

        protected override IAssemblyRule LogicalLink => this;

        public IAssemblyRule ReferenceAssembly(string name)
        {
            PredicateString.Add($"reference assembly '{name}'");
            return AppendCondition(r => r.ReferencedAssemblies.Any(x => x.Name == name));
        }

        public IAssemblyRule ReferenceAssembliesMatching(string pattern)
        {
            PredicateString.Add($"reference assemblies matching '{pattern}'");
            return AppendCondition(r => r.ReferencedAssemblies.Any(x => x.Name.Matches(pattern)));
        }

        public IAssemblyRule ReferenceAssembliesMatching(Expression<Predicate<IAssembly>> assemblyFilterExpression)
        {
            PredicateString.Add($"reference assemblies matching '{assemblyFilterExpression}'");
            var assemblyFilter = assemblyFilterExpression.Compile();
            return AppendCondition(a => a.ReferencedAssemblies.Any(r => assemblyFilter(r)));
        }
    }
}
