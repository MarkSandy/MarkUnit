using System;
using System.Collections.Generic;

namespace MarkUnit.Classes
{
    public interface IClass : IType
    {
        IEnumerable<string> ReferencedNameSpaces { get; }
        IEnumerable<IClass> ReferencedClasses { get; }
        void AddReferencedClass(IClass referencedClass);
        IEnumerable<IMethod> Methods { get; }
        IEnumerable<IMethod> Constructors { get; }
    }

    public interface IInternalClass : IClass
    {
        void AddMethod(IMethod method);
        void AddConstructor(IMethod constructor);
    }
    public interface IMethod
    {
        string MethodName { get; }
        bool IsPublic { get; }
        IClass Class { get; }
        IParameterInfo[] Parameters { get; }
    }

    internal class Method : IMethod
    {
        public Method(IClass @class,string methodName, IParameterInfo[] parameters, bool isPublic)
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
        Type ParameterType { get; }
        string ParameterName { get; }
        bool HasDefault { get; }
    }

    class MarkUnitParameterInfo : IParameterInfo
    {
        public Type ParameterType { get; }
        public string ParameterName { get; }
        public bool HasDefault { get; }

        public MarkUnitParameterInfo(Type parameterType, string parameterName, bool hasDefault)
        {
            ParameterType = parameterType;
            ParameterName = parameterName;
            HasDefault = hasDefault;
        }
    }
}