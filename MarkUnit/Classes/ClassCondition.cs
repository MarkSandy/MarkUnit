using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MarkUnit.Classes
{
    internal class ClassCondition
        : TestCollectionBase<IClass, IClassCollection, IClassTestCondition, IReducedClassCollection>,
          IClassCollection
    {
        public ClassCondition(IClassRuleFactory classRuleFactory,FilteredClasses classFilter, bool negate)
            : base(classFilter)
        {
            FilterCondition = new ClassFilterCondition(classRuleFactory,this, classFilter, negate);
        }

        public IReducedClassCollection ImplementsInterface<TInterface>()
        {
            PredicateString.Add($"implements '{typeof(TInterface)}'");
            return AppendCondition(c => c.ClassType.ImplementsInterface(typeof(TInterface)));
        }

        public IReducedClassCollection IsDerivedFrom<TClass>()
            where TClass : class
        {
            return IsDerivedFrom(typeof(TClass));
        }

        public IReducedClassCollection IsDerivedFrom(Type baseType)
        {
            PredicateString.Add($"is derived from {baseType}");
            var predicate = IsDerivedFromPredicate(baseType);
            return AppendCondition(c => predicate(c.ClassType));
        }

        public IReducedClassCollection IsDeclaredInAssemblyMatching(string pattern)
        {
            PredicateString.Add($"is declared in an assembly matching '{pattern}");
            return AppendCondition(c => c.Assembly.Name.Matches(pattern));
        }

        public IReducedClassCollection IsDeclaredInAssembly(Assembly assembly)
        {
            PredicateString.Add($"is declared in assembly '{assembly.FullName}");
            return AppendCondition(c => c.Assembly.Assembly.FullName==assembly.FullName);
        }

        public IReducedClassCollection IsDeclaredInAssembly(string name)
        {
            PredicateString.Add($"is declared in an assembly '{name}'");
            return AppendCondition(c => c.Assembly.Name == name);
        }

        public IReducedClassCollection IsDeclaredInAssembly(Expression<Predicate<Assembly>> assemblyFilterExpression)
        {
            var predicate = assemblyFilterExpression.Compile();
            PredicateString.Add($"is declared in an assembly matching '{assemblyFilterExpression}'");
            return AppendCondition(c => predicate(c.Assembly.Assembly));
        }

        public IReducedClassCollection Is(Expression<Predicate<Type>> typeExpression)
        {
            PredicateString.Add($"Is {typeExpression}");
            return AppendCondition(c => typeExpression.Compile()(c.ClassType));
        }

        public IReducedClassCollection HasAttribute<TAttribute>()
            where TAttribute : Attribute
        {
            PredicateString.Add($"has attribute '{typeof(TAttribute).Name}'");
            return AppendCondition(c => ClassHasAttribute(c.ClassType, typeof(TAttribute)));
        }

        public IReducedClassCollection ImplementsInterfaceMatching(string pattern)
        {
            PredicateString.Add($"implements an interface matching '{pattern}'");
            return AppendCondition(c => c.ClassType.GetInterfaces().Any(i => i.Name.Matches(pattern)));
        }

        private bool ClassHasAttribute(Type classType, Type attributeType)
        {
            var ad = classType.GetCustomAttributesData();
            var result = ad.Any(a => a.AttributeType.ToString() == attributeType.ToString());
            return result;
        }

        private static Predicate<Type> IsDerivedFromPredicate(Type classType)
         {
             Predicate<Type> predicate;
            if (classType.IsGenericType)
            {
                predicate = classType.IsSubclassOfRawGeneric;
            }
            else
            {
                predicate = classType.IsSubClass;
            }

            return predicate;
        }

     }
}
