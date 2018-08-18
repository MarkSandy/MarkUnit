using System.Collections.Generic;
using MarkUnit.Assemblies;

namespace MarkUnit.Classes
{
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
