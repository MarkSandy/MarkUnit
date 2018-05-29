using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MarkUnit.Classes {
    internal class TypeCondition
        : TestCollectionBase<IType, ITypeCollection, ITypeTestCondition, IReducedTypeCollection>,
          ITypeCollection
    {
        public TypeCondition(IFilteredTypes typeFilter, bool negate)
            : base(typeFilter)
        {
            FilterCondition = new TypeFilterCondition(this, typeFilter, negate);
        }

        public IReducedTypeCollection IsClass()
        {
            PredicateString.Add("is class");
            return AppendCondition(c => c.ClassType.IsClass);
        }

        public IReducedTypeCollection HasName(Expression<Predicate<string>> nameFilterExpression)
        {
            var nameFilter = nameFilterExpression.Compile();
            PredicateString.Add($"has name matching '{nameFilterExpression}'");
            return AppendCondition(c => nameFilter(c.Name));
        }

        public IReducedTypeCollection HasNameMatching(string pattern)
        {
            PredicateString.Add($"has name matching '{pattern}'");
            return AppendCondition(c => c.Name.Matches(pattern));
        }

        public IReducedTypeCollection IsInterface()
        {
            PredicateString.Add("is interface");
            return AppendCondition(c => c.ClassType.IsInterface);
        }

        public IReducedTypeCollection IsDeclaredInAssemblyMatching(string pattern)
        {
            PredicateString.Add($"is declared in an assembly matching '{pattern}");
            return AppendCondition(c => c.Assembly.Name.Matches(pattern));
        }

        public IReducedTypeCollection IsDeclaredInAssembly(Assembly assembly)
        {
            PredicateString.Add($"is declared in assembly '{assembly.FullName}");
            return AppendCondition(c => c.Assembly.Assembly.FullName==assembly.FullName);
        }

        public IReducedTypeCollection IsDeclaredInAssembly(string name)
        {
            PredicateString.Add($"is declared in an assembly '{name}'");
            return AppendCondition(c => c.Assembly.Name == name);
        }

        public IReducedTypeCollection IsDeclaredInAssembly(Expression<Predicate<Assembly>> assemblyFilterExpression)
        {
            var predicate = assemblyFilterExpression.Compile();
            PredicateString.Add($"is declared in an assembly matching '{assemblyFilterExpression}'");
            return AppendCondition(c => predicate(c.Assembly.Assembly));
        }

        public IReducedTypeCollection Is(Expression<Predicate<Type>> typeExpression)
        {
            PredicateString.Add($"Is {typeExpression}");
            return AppendCondition(c => typeExpression.Compile()(c.ClassType));
        }

        public IReducedTypeCollection HasAttribute<TAttribute>()
            where TAttribute : Attribute
        {
            PredicateString.Add($"has attribute '{typeof(TAttribute).Name}'");
            return AppendCondition(c => ClassHasAttribute(c.ClassType, typeof(TAttribute)));
        }

        public IReducedTypeCollection IsEnum()
        {
            PredicateString.Add($"is enum");
            return AppendCondition(c => c.ClassType.IsValueType); // fehlt was
        }

        public IReducedTypeCollection ImplementsInterfaceMatching(string pattern)
        {
            PredicateString.Add($"implements an interface matching '{pattern}'");
            return AppendCondition(c => c.ClassType.GetInterfaces().Any(i => StringHelper.Matches(i.Name, pattern)));
        }

        private bool ClassHasAttribute(Type classType, Type attributeType)
        {
            var ad = classType.GetCustomAttributesData();
            var result = ad.Any(a => a.AttributeType.ToString() == attributeType.ToString());
            return result;
        }

    }
}