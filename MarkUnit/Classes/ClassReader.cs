using System;
using System.Collections.Generic;
using System.Linq;
using MarkUnit.Assemblies;

namespace MarkUnit.Classes
{
    internal abstract class TypeReaderBase<T> : ITypeReader<T>
        where T : IType
    {
        private readonly Dictionary<IAssembly, T[]> _classes = new Dictionary<IAssembly, T[]>();

        protected abstract Predicate<Type> FilterFunc { get; }

        public IEnumerable<T> LoadFromAssemblies(IFilteredAssemblies assemblies)
        {
            return assemblies.FilteredItems.SelectMany(LoadFromAssembly);
        }

        private IEnumerable<T> LoadFromAssembly(IAssembly assembly)
        {
            if (!_classes.TryGetValue(assembly, out T[] classes))
            {
                classes = assembly.Assembly.GetTypes().Where(c=>FilterFunc(c)).Select(t => CreateItem(assembly,t)).ToArray();
                _classes.Add(assembly, classes);
            }

            return classes;
        }

        protected abstract T CreateItem(IAssembly asembly, Type type);
    }

    internal class ClassReader : TypeReaderBase<IClass>
    {
        protected override Predicate<Type> FilterFunc => t => t.IsClass;
        protected override IClass CreateItem(IAssembly asembly, Type type)
        {
            return new MarkUnitClass(asembly,type);
        }
    }

    internal class TypeReader : TypeReaderBase<IType>
    {
        protected override Predicate<Type> FilterFunc => t => true;
        protected override IType CreateItem(IAssembly asembly, Type type)
        {
            return new MarkUnitType(asembly,type);
        }
    }
}
