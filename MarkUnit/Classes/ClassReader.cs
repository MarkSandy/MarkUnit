using System;
using System.Collections.Generic;
using System.Linq;
using MarkUnit.Assemblies;

namespace MarkUnit.Classes
{
    internal class ClassReader : ITypeReader
    {
        private readonly Dictionary<IAssembly, IClass[]> _classes = new Dictionary<IAssembly, IClass[]>();

        public Predicate<Type> FilterFunc { get; set; }

        public IEnumerable<IClass> LoadFromAssemblies(IFilteredAssemblies assemblies)
        {
            return assemblies.FilteredItems.SelectMany(LoadFromAssembly);
        }

        private IEnumerable<IClass> LoadFromAssembly(IAssembly assembly)
        {
            if (!_classes.TryGetValue(assembly, out IClass[] classes))
            {
                if (FilterFunc == null) FilterFunc = t => true;
                classes = assembly.Assembly.GetTypes().Where(c=>FilterFunc(c)).Select(t => new MarkUnitClass(assembly, t)).ToArray();
                _classes.Add(assembly, classes);
            }

            return classes;
        }
    }
}
