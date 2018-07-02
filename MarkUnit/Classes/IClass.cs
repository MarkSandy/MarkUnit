using System.Collections.Generic;

namespace MarkUnit.Classes
{
    public interface IClass : IType
    {
        IEnumerable<string> ReferencedNameSpaces { get; }
        IEnumerable<IClass> ReferencedClasses { get; }
        void AddReferencedClass(IClass referencedClass);
    }
}