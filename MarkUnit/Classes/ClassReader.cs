using System.Collections.Generic;
using System.Linq;
using MarkUnit.Assemblies;

namespace MarkUnit.Classes
{
    internal class ClassReader : IClassReader
    {
        private readonly Dictionary<IAssembly, IClass[]> _classes = new Dictionary<IAssembly, IClass[]>();

        public IEnumerable<IClass> LoadFromAssemblies(IFilteredAssemblies assemblies)
        {
            return assemblies.FilteredItems.SelectMany(LoadFromAssembly);
        }

        private IEnumerable<IClass> LoadFromAssembly(IAssembly assembly)
        {
            if (!_classes.TryGetValue(assembly, out IClass[] classes))
            {
                classes = assembly.Assembly.GetTypes().Where(c => c.IsClass).Select(t => new MarkUnitClass(assembly, t)).ToArray();
                _classes.Add(assembly, classes);
            }

            return classes;
        }
    }
}
