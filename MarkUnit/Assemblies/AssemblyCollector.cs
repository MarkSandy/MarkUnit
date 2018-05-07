using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MarkUnit.Assemblies
{
    public class AssemblyCollector : IAssemblyCollector
    {
        private readonly IAssemblyReader _assemblyReader;
        private IEnumerable<IAssemblyInfo> _solutionAssemblies;
        private IAssembly _mainAssembly;

        public AssemblyCollector(IAssemblyReader assemblyReader)
        {
            _assemblyReader = assemblyReader;
        }

        public IAssembly MainAssembly
        {
            get => _mainAssembly;
            set
            {
                _mainAssembly = value;
                _assemblyReader.AssemblyPath = System.IO.Path.GetDirectoryName(MainAssembly?.Location);
            }
        }

        public IEnumerable<IAssemblyInfo> Get()
        {
            return ReadAllAssemblies(MainAssembly);
        }

        public IEnumerable<IAssemblyInfo> SolutionAssemblies => _solutionAssemblies ?? (_solutionAssemblies=ReadSolutionAssemblies());

        private IEnumerable<IAssemblyInfo> ReadSolutionAssemblies()
        {
            ReadAllAssemblies(MainAssembly);
            return SolutionAssemblies;
        }

        public string Pattern { get; set; }

        private IEnumerable<IAssemblyInfo> ReadAllAssemblies(IAssembly mainAssembly)
        {
            _assemblyReader.Loadall(mainAssembly);
            _solutionAssemblies = _assemblyReader.AllAssemblies.Where(a => a.Name.Matches(Pattern)).ToArray();
            return _assemblyReader.AllAssemblies;
        }
    }
}
