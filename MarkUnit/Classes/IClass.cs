using System;
using System.Collections.Generic;
using MarkUnit.Assemblies;

namespace MarkUnit.Classes
{
    public interface IClass : INamedComponent
    {
        Type ClassType { get; }
        IAssembly Assembly { get; }
        IEnumerable<string> ReferencedNameSpaces { get; }
        IEnumerable<IClass> ReferencedClasses { get; }
        void AddReferencedClass(IClass referencedClass);
    }
}