using System.Collections.Generic;
using System.Linq;
using MarkUnit.Assemblies;

namespace MarkUnit.Classes 
{
    internal class ClassCollector : IClassCollector
    {
        private readonly ITypeReader _classReader;
        private readonly IClassInfoCollector _classInfoCollector;

        public ClassCollector(ITypeReader classReader, IClassInfoCollector classInfoCollector)
        {
            _classReader = classReader;
            _classReader.FilterFunc = t => t.IsClass;
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

    internal class TypeCollector : ITypeCollector
    {
        private readonly ITypeReader _classReader;

        public TypeCollector(ITypeReader classReader)
        {
            _classReader = classReader;
            _classReader.FilterFunc = t => true;
        }

        public IFilteredAssemblies Assemblies { get; set; }

        public IEnumerable<IClass> Get()
        {
            return _classReader.LoadFromAssemblies(Assemblies).Select(Examine);
        }

        private IClass Examine(IClass classInfo)
        {
              return classInfo;
        }
    }
}