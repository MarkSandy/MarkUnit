using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MarkUnit.Assemblies
{
    public class AssemblyCollector : IAssemblyCollector
    {
        private readonly IAssemblyReader _assemblyReader;
        private IEnumerable<IAssembly> _solutionAssemblies;

        public AssemblyCollector(IAssemblyReader assemblyReader)
        {
            _assemblyReader = assemblyReader;
        }

        public Assembly MainAssembly { get; set; }

        public IEnumerable<IAssembly> Get()
        {
            return ReadAllAssemblies(MainAssembly);
        }

        public IEnumerable<IAssembly> SolutionAssemblies => null;
        public string Pattern { get; set; }

        private IEnumerable<IAssembly> ReadAllAssemblies(Assembly mainAssembly)
        {
            _assemblyReader.Loadall(mainAssembly);
            _solutionAssemblies = _assemblyReader.AllAssemblies.Where(a => a.Name.Matches(Pattern)).ToArray();
            return _assemblyReader.AllAssemblies;
        }
    }
}
