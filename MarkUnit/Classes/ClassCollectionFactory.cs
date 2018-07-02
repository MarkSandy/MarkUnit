namespace MarkUnit.Classes
{
    internal class ClassCollectionFactory : IClassCollectionFactory
    {
        
        public IClassCollection Create(IClassRuleFactory classRuleFactory,IClassCollector classCollector, bool negate, bool not)
        {
            var classes = classCollector.Get();
            var classFilter = new FilteredClasses(classes);
            var result = new ClassCondition(classRuleFactory, classFilter, negate);
            return not ? result.SilentNot() : result;
        }
    }
}