namespace MarkUnit.Classes
{
    internal class ClassFilterCondition
        : FilterConditionBase<IClassCollection, IClassTestCondition, IClass>,
          IClassBinaryOperator
    {
        public ClassFilterCondition(IClassRuleFactory classRuleFactory, IClassCollection condition, IFilter<IClass> filter, bool negate)
            : base(condition, filter, negate)
        {
            CeateAssertionFunc = classRuleFactory.Create;
        }
    }
}
