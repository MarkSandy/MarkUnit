using System;
using System.Collections.Generic;
using System.Linq;
using MarkUnit.Assemblies;

namespace MarkUnit.Classes
{
    internal class TypeReader : ITypeReader<IType>
    {
        private readonly Dictionary<IAssembly, IType[]> _classes = new Dictionary<IAssembly, IType[]>();


        public IEnumerable<IType> LoadFromAssemblies(IFilteredAssemblies assemblies)
        {
            return assemblies.FilteredItems.SelectMany(LoadFromAssembly);
        }

        private IEnumerable<IType> LoadFromAssembly(IAssembly assembly)
        {
            if (!_classes.TryGetValue(assembly, out IType[] classes))
            {
                classes = assembly.Assembly.GetTypes().Select(t => new MarkUnitType(assembly,t)).ToArray();
                _classes.Add(assembly, classes);
            }

            return classes;
        }
    }

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
