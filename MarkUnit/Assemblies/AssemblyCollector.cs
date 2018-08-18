using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MarkUnit.Assemblies
{
    public class AssemblyCollector : IAssemblyCollector
    {
        private readonly IAssemblyReader _assemblyReader;
        private Assembly _mainAssembly;
        private IEnumerable<IAssembly> _solutionAssemblies;

        public AssemblyCollector(IAssemblyReader assemblyReader)
        {
            _assemblyReader = assemblyReader;
        }

        public Assembly MainAssembly
        {
            get => _mainAssembly;
            set
            {
                _mainAssembly = value;
                _assemblyReader.AssemblyPath = Path.GetDirectoryName(_mainAssembly.Location);
            }
        }

        public IEnumerable<IAssembly> Get()
        {
            return ReadAllAssemblies(MainAssembly);
        }

        public IEnumerable<IAssembly> SolutionAssemblies
        {
            get
            {
                if (_solutionAssemblies == null) Get();
                return _solutionAssemblies;
            }
        }

        public string Pattern { get; set; }

        private IEnumerable<IAssembly> ReadAllAssemblies(Assembly mainAssembly)
        {
            _assemblyReader.Loadall(mainAssembly);
            _solutionAssemblies = _assemblyReader.AllAssemblies.Where(a => a.Name.Matches(Pattern)).ToArray();
            return _assemblyReader.AllAssemblies;
        }
    }
}
