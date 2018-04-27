using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MarkUnit.Assemblies
{
    public class AssemblyCollector : IAssemblyCollector
    {
        private readonly IAssemblyReader _assemblyReader;
        private IEnumerable<IAssemblyInfo> _solutionAssemblies;

        public AssemblyCollector(IAssemblyReader assemblyReader)
        {
            _assemblyReader = assemblyReader;
        }

        public IAssembly MainAssembly { get; set; }

        public IEnumerable<IAssemblyInfo> Get()
        {
            return ReadAllAssemblies(MainAssembly);
        }

        public IEnumerable<IAssemblyInfo> SolutionAssemblies => null;
        public string Pattern { get; set; }

        private IEnumerable<IAssemblyInfo> ReadAllAssemblies(IAssembly mainAssembly)
        {
            _assemblyReader.Loadall(mainAssembly);
            _solutionAssemblies = _assemblyReader.AllAssemblies.Where(a => a.Name.Matches(Pattern)).ToArray();
            return _assemblyReader.AllAssemblies;
        }
    }
}
