using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MarkUnit.Classes
{
    internal class ClassCondition
        : TestCollectionBase<IClassInfo, IClassCollection, IClassTestCondition, IReducedClassCollection>,
          IClassCollection
    {
        public ClassCondition(FilteredClasses classFilter, bool negate)
            : base(classFilter)
        {
            FilterCondition = new ClassFilterCondition(this, classFilter, negate);
        }

        public IReducedClassCollection HasName(Expression<Predicate<string>> nameFilterExpression)
        {
            var nameFilter = nameFilterExpression.Compile();
            PredicateString.Add($"has name matching '{nameFilterExpression}'");
            return AppendCondition(c => nameFilter(c.Name));
        }

        public IReducedClassCollection HasNameMatching(string pattern)
        {
            PredicateString.Add($"has name matching '{pattern}'");
            return AppendCondition(c => c.Name.Matches(pattern));
        }

        public IReducedClassCollection ImplementsInterface<TInterface>()
        {
            PredicateString.Add($"implements '{typeof(TInterface)}'");
            return AppendCondition(c => typeof(TInterface).IsAssignableFrom(c.ClassType));
        }

        public IReducedClassCollection IsDerivedFrom<TClass>()
            where TClass : class
        {
            return IsDerivedFrom(typeof(TClass));
        }

        public IReducedClassCollection IsDerivedFrom(Type type)
        {
            PredicateString.Add($"is derived from {type}");
            var predicate = CreateClassPredicate(type);
            return AppendCondition(c => predicate(c.ClassType));
        }

        public IReducedClassCollection IsDeclaredInAssemblyMatching(string pattern)
        {
            PredicateString.Add($"is declared in an assembly matching '{pattern}");
            return AppendCondition(c => c.Assembly.Name.Matches(pattern));
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

        private static Predicate<Type> CreateClassPredicate(Type classType)
         {
             Predicate<Type> predicate;
            if (classType.IsGenericType)
            {
                predicate = t => IsSubclassOfRawGeneric(classType, t);
            }
            else
            {
                predicate = t => t.IsSubclassOf(classType);
            }

            return predicate;
        }

        private static bool IsSubclassOfRawGeneric(Type generic, Type toCheck)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var currentType = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == currentType)
                {
                    return true;
                }

                toCheck = toCheck.BaseType;
            }

            return false;
        }
    }
}
