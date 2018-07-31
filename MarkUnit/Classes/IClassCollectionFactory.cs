namespace MarkUnit.Classes
{
    internal interface IClassCollectionFactory
    {
        ClassCondition Create(IClassRuleFactory classRuleFactory, IClassCollector classCollector, bool negate, bool not, string[] exceptions);
    }
}