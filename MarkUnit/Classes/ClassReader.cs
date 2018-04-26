using System.Collections.Generic;
using System.Linq;
using MarkUnit.Assemblies;

namespace MarkUnit.Classes
{
    internal class ClassReader : IClassReader
    {
        private readonly Dictionary<IAssemblyInfo, IClassInfo[]> _classes = new Dictionary<IAssemblyInfo, IClassInfo[]>();

        public IEnumerable<IClassInfo> LoadFromAssemblies(IFilteredAssemblies assemblies)
        {
            return assemblies.FilteredItems.SelectMany(LoadFromAssembly);
        }

        private IEnumerable<IClassInfo> LoadFromAssembly(IAssemblyInfo assembly)
        {
            if (!_classes.TryGetValue(assembly, out IClassInfo[] classes))
            {
                classes = assembly.Assembly.GetTypes().Where(c => c.IsClass).Select(t => new MarkUnitClass(assembly, t)).ToArray();
                _classes.Add(assembly, classes);
            }

            return classes;
        }
    }
}
