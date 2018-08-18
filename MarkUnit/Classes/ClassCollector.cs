using System.Collections.Generic;
using System.Linq;
using MarkUnit.Assemblies;

namespace MarkUnit.Classes
{
    internal class ClassCollector : IClassCollector
    {
        private readonly IClassInfoCollector _classInfoCollector;
        private readonly ITypeReader<IInternalClass> _classReader;

        public ClassCollector(ITypeReader<IInternalClass> classReader, IClassInfoCollector classInfoCollector)
        {
            _classReader = classReader;
            _classInfoCollector = classInfoCollector;
        }

        public IFilteredAssemblies Assemblies { get; set; }

        public IEnumerable<IClass> Get()
        {
            return _classReader.LoadFromAssemblies(Assemblies).Select(Examine);
        }

        private IClass Examine(IInternalClass classInfo)
        {
            return _classInfoCollector.Examine(classInfo);
        }
    }
}
