using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MarkUnit.Assemblies;

namespace MarkUnit.Classes 
{
    internal class ClassCollector : IClassCollector
    {
        private readonly ITypeReader<IInternalClass> _classReader;
        private readonly IClassInfoCollector _classInfoCollector;

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