using System.Collections.Generic;
using System.Linq;
using MarkUnit.Assemblies;

namespace MarkUnit.Classes {
    internal class ClassCollector : IClassCollector
    {
        private readonly IClassReader _classReader;
        private readonly IClassInfoCollector _classInfoCollector;

        public ClassCollector(IClassReader classReader, IClassInfoCollector classInfoCollector)
        {
            _classReader = classReader;
            _classInfoCollector = classInfoCollector;
        }

        public IFilteredAssemblies Assemblies { get; set; }

        public IEnumerable<IClass> Get()
        {
            return _classReader.LoadFromAssemblies(Assemblies).Select(Examine);
        }

        private IClass Examine(IClass arg)
        {
            _classInfoCollector.Examine(arg);
            return arg;
        }
    }
}