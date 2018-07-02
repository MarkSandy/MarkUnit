namespace MarkUnit.Classes
{
    internal interface IInterfaceCollectionFactory
    {
        IInterfaceCollection Create(IInterfaceRuleFactory classRuleFactory,IInterfaceCollector classCollector, bool negate, bool not);
    }
}