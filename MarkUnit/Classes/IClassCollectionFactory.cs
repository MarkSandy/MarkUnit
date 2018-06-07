namespace MarkUnit.Classes
{
    internal interface IClassCollectionFactory
    {
        IClassCollection Create(IClassRuleFactory classRuleFactory,IClassCollector classCollector, bool negate, bool not);
    }

    internal class ClassCollectionFactory : IClassCollectionFactory
    {
        
        public IClassCollection Create(IClassRuleFactory classRuleFactory,IClassCollector classCollector, bool negate, bool not)
        {
            var classes = classCollector.Get();
            var classFilter = new FilteredClasses(classes);
            var result = new ClassCondition(classRuleFactory, classFilter, negate);
            return not ? result.Not() : result;
        }
    }

    internal interface ITypeCollectionFactory
    {
        ITypeCollection Create(ITypeCollector typeCollector, bool negate, bool not);
    }

    internal class TypeCollectionFactory : ITypeCollectionFactory
    {
        public ITypeCollection Create(ITypeCollector typeCollector, bool negate, bool not)
        {
            var types = typeCollector.Get();
            var typeFilter = new FilteredTypes(types);
            var result = new TypeCondition(new ClassCollectionFactory(), typeFilter, negate);
            return not ? result.Not() : result;
        }
    }
}