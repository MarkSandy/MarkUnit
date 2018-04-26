namespace MarkUnit.Classes
{
    internal class ClassFilterCondition
        : FilterConditionBase<IClassCollection, IClassTestCondition, IClassInfo>,
          IClassBinaryOperator
    {
        public ClassFilterCondition(IClassCollection condition, IFilter<IClassInfo> filter, bool negate)
            : base(condition, filter, negate)
        {
            CeateAssertionFunc = (f, b) => new ClassRule(f, b);
        }
    }
}
