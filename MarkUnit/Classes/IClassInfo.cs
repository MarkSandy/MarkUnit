using System;
using System.Collections.Generic;
using MarkUnit.Assemblies;

namespace MarkUnit.Classes
{
    public interface IClassInfo : INamedComponent
    {
        Type ClassType { get; }
        IAssemblyInfo Assembly { get; }
        IEnumerable<string> ReferencedNameSpaces { get; }
        IEnumerable<IClassInfo> ReferencedClasses { get; }
        string Namespace { get; }
        void AddReferencedClass(IClassInfo referencedClass);
    }
}