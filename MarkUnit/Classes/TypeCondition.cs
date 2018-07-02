using System.Linq;

namespace MarkUnit.Classes 
{
    internal class TypeCondition
        : TypeConditionBase<IType, ITypeCollection, ITypeTestCondition, IReducedTypeCollection>,
          ITypeCollection
    {
        private readonly IClassCollectionFactory _classCollectionFactory;
        private readonly IInterfaceCollectionFactory _interfaceCollectionFactory;

        public TypeCondition(IClassCollectionFactory classCollectionFactory, 
            
            IInterfaceCollectionFactory interfaceCollectionFactory,
            IFilteredTypes typeFilter, bool negate)
            : base(typeFilter)
        {
            _classCollectionFactory = classCollectionFactory;
            _interfaceCollectionFactory = interfaceCollectionFactory;
            FollowUp = this;
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

        public IReducedInterfaceCollection IsInterface()
        {
            PredicateString.Add("is interface");
            return CreateReducedInterfaceCollection(Filter);
        }

        private IReducedInterfaceCollection CreateReducedInterfaceCollection(IFilter<IType> filter)
        {
            var interfaceCollector = new InterfaceFromTypeCollector(filter);
            var classCollection = _interfaceCollectionFactory.Create(Instances.InterfaceRuleFactory, interfaceCollector, FilterCondition.Negate, false);
            var classFilter = new FilteredInterfaces(interfaceCollector.Get());
            return new InterfaceFilterCondition(Instances.InterfaceRuleFactory, classCollection, classFilter, FilterCondition.Negate);
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
    }
}