using System;
using System.Collections.Generic;
using System.Linq;
using MarkUnit.Assemblies;

namespace MarkUnit.Classes
{
    internal class ClassReader : ITypeReader<IClass>
    {
        private readonly ITypeReader<IType> _typeReader;

        public ClassReader(ITypeReader<IType> typeReader)
        {
            _typeReader = typeReader;
        }

        public IEnumerable<IClass> LoadFromAssemblies(IFilteredAssemblies assemblies)
        {
            return _typeReader.LoadFromAssemblies(assemblies)
                .Where(t=>t.ClassType.IsClass)
                .Select(t => new MarkUnitClass(t.Assembly, t.ClassType));
        }
    }
}
