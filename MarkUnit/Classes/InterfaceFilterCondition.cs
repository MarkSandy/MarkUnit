namespace MarkUnit.Classes
{
    internal class InterfaceFilterCondition
        : FilterConditionBase<IInterfaceCollection, IInterfaceTestCondition, IInterface>,
            IInterfaceBinaryOperator
    {
        public InterfaceFilterCondition(IInterfaceRuleFactory interfaceRuleFactory, IInterfaceCollection condition, IFilter<IInterface> filter, bool negate)
            : base(condition, filter, negate)
        {
            CeateAssertionFunc = interfaceRuleFactory.Create;
        }
    }
}