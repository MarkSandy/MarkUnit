namespace MarkUnit.Classes
{
    internal interface ITypeRuleFactory
    {
        TypeRule Create(IFilter<IType> filter, bool negateAssertion);
    }
}
