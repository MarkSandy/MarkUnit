namespace MarkUnit.Classes
{
    internal interface IInterfaceRuleFactory
    {
        InterfaceRule Create(IFilter<IInterface> filter, bool negateAssertion);
    }
}
