using System;
using System.Collections.Generic;

namespace MarkUnit.Classes
{
    public interface IClass : IType
    {
        IEnumerable<IMethod> Constructors { get; }
        IEnumerable<IMethod> Methods { get; }
        IEnumerable<IClass> ReferencedClasses { get; }
        IEnumerable<string> ReferencedNameSpaces { get; }
        void AddReferencedClass(IClass referencedClass);
    }

    public interface IInternalClass : IClass
    {
        void AddConstructor(IMethod constructor);
        void AddMethod(IMethod method);
    }

    public interface IMethod
    {
        IClass Class { get; }
        bool IsPublic { get; }
        string MethodName { get; }
        IParameterInfo[] Parameters { get; }
    }

    internal class Method : IMethod
    {
        public Method(IClass @class, string methodName, IParameterInfo[] parameters, bool isPublic)
        {
            MethodName = methodName;
            Parameters = parameters;
            IsPublic = isPublic;
            Class = @class;
        }

        public string MethodName { get; }
        public bool IsPublic { get; }
        public IClass Class { get; }
        public IParameterInfo[] Parameters { get; }
    }

    public interface IParameterInfo
    {
        bool HasDefault { get; }
        string ParameterName { get; }
        Type ParameterType { get; }
    }

    class MarkUnitParameterInfo : IParameterInfo
    {
        public MarkUnitParameterInfo(Type parameterType, string parameterName, bool hasDefault)
        {
            ParameterType = parameterType;
            ParameterName = parameterName;
            HasDefault = hasDefault;
        }

        public Type ParameterType { get; }
        public string ParameterName { get; }
        public bool HasDefault { get; }
    }
}
