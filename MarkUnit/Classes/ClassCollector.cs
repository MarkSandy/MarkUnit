using System.Collections.Generic;
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

    internal class TypeCollector : ITypeCollector
    {
        private readonly ITypeReader<IType> _typeReader;

        public TypeCollector(ITypeReader<IType> typeReader)
        {
            _typeReader = typeReader;
        }

        public IFilteredAssemblies Assemblies { get; set; }

        public IEnumerable<IType> Get()
        {
            return _typeReader.LoadFromAssemblies(Assemblies);
        }
    }
}