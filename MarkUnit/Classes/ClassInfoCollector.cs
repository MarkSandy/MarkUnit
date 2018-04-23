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
        private readonly Dictionary<Type, IClass> _classes = new Dictionary<Type, IClass>();

        public ClassInfoCollector(IAssemblyReader assemblyReader)
        {
            _assemblyReader = assemblyReader;
        }

        public void Examine(IClass classInfo)
        {
            if (_classes.ContainsKey(classInfo.ClassType)) return;
            CollectInfoFromConstructors(classInfo);
            CollectInfoFromFields(classInfo);
            CollectInfoFromMethods(classInfo);
            Add(classInfo, classInfo.ClassType);
        }

        private void Add(IClass classInfo, Type type)
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

        private void CollectInfoFromConstructors(IClass classInfo)
        {
            CollectClassInfoFromConstructors(classInfo, BindingFlags.Instance | BindingFlags.Public);
            CollectClassInfoFromConstructors(classInfo, BindingFlags.Instance | BindingFlags.NonPublic);
        }

        private void CollectClassInfoFromConstructors(IClass classInfo, BindingFlags bindingFlags)
        {
            foreach (ConstructorInfo constructorInfo in classInfo.ClassType.GetConstructors(bindingFlags))
            {
                CollectFromParameters(classInfo, constructorInfo.GetParameters());
                CollectFromMethodBody(classInfo, constructorInfo.GetMethodBody());
            }
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

        private void CollectInfoFromMethods(IClass classInfo)
        {
            CollectInfoFromMethods(classInfo, BindingFlags.Instance | BindingFlags.Public);
            CollectInfoFromMethods(classInfo, BindingFlags.Static | BindingFlags.Public);
            CollectInfoFromMethods(classInfo, BindingFlags.Instance | BindingFlags.NonPublic);
            CollectInfoFromMethods(classInfo, BindingFlags.Static | BindingFlags.NonPublic);
        }

        private void CollectInfoFromMethods(IClass classInfo, BindingFlags bindingFlags)
        {
            foreach (MethodInfo methodInfo in classInfo.ClassType.GetMethods(bindingFlags))
            {
                Collect(classInfo, methodInfo.ReturnType);
                CollectFromParameters(classInfo, methodInfo.GetParameters());
                CollectFromMethodBody(classInfo, methodInfo.GetMethodBody());
            }
        }

        private IClass Get(Type type)
        {
            if (!_classes.TryGetValue(type, out IClass result))
            {
                result = new MarkUnitClass(_assemblyReader.LoadAssembly(type.Assembly), type);
            }

            return result;
        }
    }
}
