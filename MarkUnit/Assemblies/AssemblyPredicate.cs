namespace MarkUnit.Assemblies
{
    public class AssemblyPredicate : IAssemblyPredicate
    {
        private readonly IAssemblyCollector _assemblyCollector;
        private readonly bool _negate;

        public AssemblyPredicate(IAssemblyCollector assemblyCollector, bool negate)
        {
            _assemblyCollector = assemblyCollector;
            _negate = negate;
        }

        public IAssemblyCollection That()
        {
            PredicateString.Add("that");
            var assemblies = _assemblyCollector.Get();
            var assemblyFilter = new FilteredAssemblies(assemblies);
            return new AssemblyCollection(assemblyFilter, _negate);
        }
    }
}