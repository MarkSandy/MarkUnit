using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using MarkUnit.Assemblies;

namespace MarkUnit.Classes {
    internal class TypeCondition
        : TestCollectionBase<IType, ITypeCollection, ITypeTestCondition, IReducedTypeCollection>,
          ITypeCollection
    {
        private readonly IClassCollectionFactory _classCollectionFactory;

        public TypeCondition(IClassCollectionFactory classCollectionFactory, IFilteredTypes typeFilter, bool negate)
            : base(typeFilter)
        {
            _classCollectionFactory = classCollectionFactory;
            FilterCondition = new TypeFilterCondition(this, typeFilter, negate);
        }

        public IReducedClassCollection IsClass()
        {
            PredicateString.Add("is class");
            AppendCondition(c => c.ClassType.IsClass);

            return CreateReducedClassCollection(Filter);
        }

        private IReducedClassCollection CreateReducedClassCollection(IFilter<IType> filter)
        {
            var classCollector = new ClassFromTypeCollector(filter);
            var classCollection = _classCollectionFactory.Create(Instances.ClassRuleFactory, classCollector, FilterCondition.Negate, false);
            var classFilter = new FilteredClasses(classCollector.Get());
            return new ClassFilterCondition(Instances.ClassRuleFactory, classCollection, classFilter, FilterCondition.Negate);
        }

        //public IReducedTypeCollection HasName(Expression<Predicate<string>> nameFilterExpression)
        //{
        //    var nameFilter = nameFilterExpression.Compile();
        //    PredicateString.Add($"has name matching '{nameFilterExpression}'");
        //    return AppendCondition(c => nameFilter(c.Name));
        //}

        //public IReducedTypeCollection HasNameMatching(string pattern)
        //{
        //    PredicateString.Add($"has name matching '{pattern}'");
        //    return AppendCondition(c => c.Name.Matches(pattern));
        //}

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
    
    internal class ClassFromTypeCollector : IClassCollector
    {
        private readonly IEnumerable<IClass> _classes;

        public ClassFromTypeCollector(IFilter<IType> filter)
        {
            _classes = filter.FilteredItems.Select(t => new MarkUnitClass(t.Assembly, t.ClassType));
        }

        public IEnumerable<IClass> Get()
        {
            return _classes;
        }

        public IFilteredAssemblies Assemblies { get; set; }
    }
}