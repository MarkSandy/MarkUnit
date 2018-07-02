using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MarkUnit.Assemblies;

namespace MarkUnit.Classes 
{
    internal class ClassCollector : IClassCollector
    {
        private readonly ITypeReader<IClass> _classReader;
        private readonly IClassInfoCollector _classInfoCollector;

        public ClassCollector(ITypeReader<IClass> classReader, IClassInfoCollector classInfoCollector)
        {
            _classReader = classReader;
            _classInfoCollector = classInfoCollector;
        }

        public IFilteredAssemblies Assemblies { get; set; }

        public IEnumerable<IClass> Get()
        {
            return _classReader.LoadFromAssemblies(Assemblies).Select(Examine);
        }

        private IClass Examine(IClass classInfo)
        {
            _classInfoCollector.Examine(classInfo);
            return classInfo;
        }
    }
}