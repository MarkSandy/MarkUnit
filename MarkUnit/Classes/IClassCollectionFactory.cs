namespace MarkUnit.Classes
{
    internal interface IClassCollectionFactory
    {
        IClassCollection Create(IClassRuleFactory classRuleFactory,IClassCollector classCollector, bool negate, bool not);
    }
}