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
                try
                {
                    classes = assembly.Assembly.GetTypes().Select(t => new MarkUnitType(assembly, t)).ToArray();
                }
                catch (System.Reflection.ReflectionTypeLoadException ex)
                {
                    {
                        classes=ex.Types.Where(t=>t!=null).Select(t => new MarkUnitType(assembly, t)).ToArray();
                        var loaderExceptions  = ex.LoaderExceptions;
                        // TODO
                    }
                }
                _classes.Add(assembly, classes);
            }

            return classes;
        }
    }
}