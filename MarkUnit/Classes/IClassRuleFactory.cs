namespace MarkUnit.Classes
{
    internal interface IClassRuleFactory
    {
        ClassRule Create(IFilter<IClass> filter, bool negateAssertion);
    }
}
