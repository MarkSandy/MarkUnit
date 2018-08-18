namespace MarkUnit.Classes
{
    internal class InterfaceCollectionFactory : IInterfaceCollectionFactory
    {
        public IInterfaceCollection Create(IInterfaceRuleFactory classRuleFactory, IInterfaceCollector classCollector, bool negate, bool not)
        {
            var classes = classCollector.Get();
            var classFilter = new FilteredInterfaces(classes);
            var result = new InterfaceCondition(classRuleFactory, classFilter, negate);
            return result.AddIgnoreList(new string[0], not);
        }
    }
}
