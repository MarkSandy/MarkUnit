namespace MarkUnit.Assemblies
{
    public class AssemblyPredicate : IAssemblyPredicate
    {
        private readonly IAssemblyCollector _assemblyCollector;
        private readonly bool _negate;
        private readonly bool _not;

        public AssemblyPredicate(IAssemblyCollector assemblyCollector, bool negate, bool not)
        {
            _assemblyCollector = assemblyCollector;
            _negate = negate;
            _not = not;
        }

        public IAssemblyCollection That()
        {
            return ThatOrWhich("that");
        }

        public IAssemblyCollection Which()
        {
            return ThatOrWhich("which");
        }

        private IAssemblyCollection ThatOrWhich(string word)
        {
            PredicateString.Add(word);
            var assemblies = _assemblyCollector.Get();
            var assemblyFilter = new FilteredAssemblies(assemblies);
            var result= new AssemblyCollection(assemblyFilter, _negate);
            return _not ? result.Not() : result;
        }
    }
}
