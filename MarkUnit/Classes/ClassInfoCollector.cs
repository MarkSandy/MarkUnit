using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MarkUnit.Assemblies;

namespace MarkUnit.Classes
{
    internal class ClassInfoCollector
        : IClassInfoCollector
    {
        private readonly IAssemblyReader _assemblyReader;
        private readonly Dictionary<Type, IInternalClass> _classes = new Dictionary<Type, IInternalClass>();

        public ClassInfoCollector(IAssemblyReader assemblyReader)
        {
            _assemblyReader = assemblyReader;
        }

        public IInternalClass Examine(IInternalClass classInfo)
        {
            if (_classes.TryGetValue(classInfo.ClassType, out IInternalClass result))
            {
                return result;
            }
            CollectInfoFromConstructors(classInfo);
            CollectInfoFromFields(classInfo);
            CollectInfoFromMethods(classInfo);
            Add(classInfo, classInfo.ClassType);
            return classInfo;
        }

        private void Add(IInternalClass classInfo, Type type)
        {
            if (!_classes.ContainsKey(type)) _classes.Add(type, classInfo);
        }

        private void Collect(IClass classType, Type type)
        {
            if (type.IsGenericParameter || type == typeof(void)) return;
            var referencedClass = Get(type);
            classType.AddReferencedClass(referencedClass);
        }

        private void CollectFromMethodBody(IClass classInfo, MethodBody methodBody)
        {
            if (methodBody != null)
            {
                foreach (LocalVariableInfo localVariable in methodBody.LocalVariables)
                {
                    Collect(classInfo, localVariable.LocalType);
                }
            }
        }

        private void CollectFromParameters(IClass classInfo, ParameterInfo[] parameters)
        {
            foreach (ParameterInfo parameterInfo in parameters)
            {
                Collect(classInfo, parameterInfo.ParameterType);
            }
        }

        private void CollectInfoFromConstructors(IInternalClass classInfo)
        {
            CollectClassInfoFromConstructors(classInfo, BindingFlags.Instance | BindingFlags.Public);
            CollectClassInfoFromConstructors(classInfo, BindingFlags.Instance | BindingFlags.NonPublic);
        }

        private void CollectClassInfoFromConstructors(IInternalClass classInfo, BindingFlags bindingFlags)
        {
            foreach (ConstructorInfo constructorInfo in classInfo.ClassType.GetConstructors(bindingFlags))
            {
                classInfo.AddConstructor(CreateMethodInfo(classInfo, constructorInfo));
                CollectFromParameters(classInfo, constructorInfo.GetParameters());
                CollectFromMethodBody(classInfo, constructorInfo.GetMethodBody());
            }
        }

        private IMethod CreateMethodInfo(IInternalClass classInfo, ConstructorInfo constructorInfo)
        {
            return new Method(classInfo,"ctor",CreateParameterList(constructorInfo.GetParameters()),constructorInfo.IsPublic);
        }

        private IParameterInfo[] CreateParameterList(ParameterInfo[] parameter)
        {
            return parameter.Select(CreateParameterInfo).ToArray();
        }

        private IParameterInfo CreateParameterInfo (ParameterInfo p)
        {
            return new MarkUnitParameterInfo(p.ParameterType, p.Name, p.IsOptional);
        }

        private void CollectInfoFromFields(IClass classInfo)
        {
            CollectInfoFromFields(classInfo, BindingFlags.Static | BindingFlags.NonPublic);
            CollectInfoFromFields(classInfo, BindingFlags.Instance | BindingFlags.NonPublic);
        }

        private void CollectInfoFromFields(IClass classInfo, BindingFlags bindingFlags)
        {
            foreach (FieldInfo fieldInfo in classInfo.ClassType.GetFields(bindingFlags))
            {
                Collect(classInfo, fieldInfo.FieldType);
            }
        }

        private void CollectInfoFromMethods(IInternalClass classInfo)
        {
            CollectInfoFromMethods(classInfo, BindingFlags.Instance | BindingFlags.Public);
            CollectInfoFromMethods(classInfo, BindingFlags.Static | BindingFlags.Public);
            CollectInfoFromMethods(classInfo, BindingFlags.Instance | BindingFlags.NonPublic);
            CollectInfoFromMethods(classInfo, BindingFlags.Static | BindingFlags.NonPublic);
        }

        private void CollectInfoFromMethods(IInternalClass classInfo, BindingFlags bindingFlags)
        {
            foreach (MethodInfo methodInfo in classInfo.ClassType.GetMethods(bindingFlags))
            {
                classInfo.AddMethod(CreateMethodInfo(classInfo, methodInfo));
                Collect(classInfo, methodInfo.ReturnType);
                CollectFromParameters(classInfo, methodInfo.GetParameters());
                CollectFromMethodBody(classInfo, methodInfo.GetMethodBody());
            }
        }

        private IMethod CreateMethodInfo(IInternalClass classInfo,MethodInfo methodInfo)
        {
             return new Method(classInfo,methodInfo.Name, CreateParameterList(methodInfo.GetParameters()),methodInfo.IsPublic);
        }

        private IClass Get(Type type)
        {
            if (!_classes.TryGetValue(type, out IInternalClass result))
            {
                result = new MarkUnitClass(_assemblyReader.LoadAssembly(type.Assembly), type);
            }

            return result;
        }
    }
}
