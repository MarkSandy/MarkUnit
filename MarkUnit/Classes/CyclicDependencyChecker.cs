using System.Collections.Generic;
using System.Linq;

namespace MarkUnit.Classes
{
    internal class CyclicDependencyChecker
    {
        private readonly Dictionary<string, bool> _knownDependencies = new Dictionary<string, bool>();

        private readonly List<IClass> _path = new List<IClass>();
        public bool HasCyclicDependencies(IClass classInfo)
        {
            _path.Clear();
            var result = ReferencesIndirect(classInfo, classInfo.Namespace);
            // Path now contains dependency path
            return result;
        }

        private static string CreateKey(IClass classInfo, string nameSpace)
        {
            return $"{classInfo.FullName}->{nameSpace}";
        }

        private bool ReferencesIndirect(IClass classInfo, string nameSpace)
        {
            var key = CreateKey(classInfo, nameSpace);
            if (!_knownDependencies.TryGetValue(key, out bool isDependent)) return true;
            {
                isDependent = classInfo.ReferencedClasses.Any(c => !c.IsNative && ReferencesNamespace(c, nameSpace));
                _knownDependencies.Add(key, isDependent);

            }
            if (isDependent) _path.Add(classInfo);
            return isDependent;
        }

        private bool ReferencesNamespace(IClass classInfo, string nameSpace)
        {
            if (ReferencesDirect(classInfo, nameSpace))
            {
                AppendDependendClassesToPath(classInfo, nameSpace);
                return true;
            }
            return ReferencesIndirect(classInfo, nameSpace);
            // return ReferencesDirect(classInfo, nameSpace) || ReferencesIndirect(classInfo, nameSpace);
        }

        private void AppendDependendClassesToPath(IClass classInfo, string nameSpace)
        {
            var c = classInfo.ReferencedClasses.FirstOrDefault(x => x.Namespace == nameSpace);
            _path.Add(c);
            _path.Add(classInfo);
        }

        private static bool ReferencesDirect(IClass classInfo, string nameSpace)
        {
            return classInfo.Namespace != nameSpace && classInfo.ReferencedNameSpaces.Contains(nameSpace);
        }
    }
}