using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MarkUnit.Classes
{
    internal class CyclicDependencyChecker
    {
        private readonly Dictionary<string, bool> _knownDependencies = new Dictionary<string, bool>();

        public bool HasCyclicDependencies(IClass classInfo)
        {
            return ReferencesIndirect(classInfo, classInfo.Namespace);
        }

        private void AddKnownDependency(IClass classInfo, string nameSpace, bool isDependent)
        {
            var key = $"{classInfo.FullName}->{nameSpace}";
            if (!_knownDependencies.ContainsKey(key))
            {
                _knownDependencies.Add(key, isDependent);
            }
        }

        private bool IsKnownDependency(IClass classInfo, string nameSpace, out bool result)
        {
            var key = $"{classInfo.FullName}->{nameSpace}";
            if (_knownDependencies.TryGetValue(key, out result)) return true;
            result = false;
            return false;
        }

        private bool ReferencesIndirect(IClass classInfo, string nameSpace)
        {
            if (!IsKnownDependency(classInfo, nameSpace, out bool result))
            {
                result = classInfo.ReferencedClasses.Any(c => ReferencesNamespace(c, nameSpace));
                AddKnownDependency(classInfo, nameSpace, result);
            }
            return result;
        }

        private bool ReferencesNamespace(IClass classInfo, string nameSpace)
        {
            return (classInfo.Namespace != nameSpace && classInfo.ReferencedNameSpaces.Contains(nameSpace))
                   || ReferencesIndirect(classInfo, nameSpace);
        }
    }
}