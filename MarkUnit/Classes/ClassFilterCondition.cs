namespace MarkUnit.Classes
{
    internal class ClassFilterCondition
        : FilterConditionBase<IClassCollection, IClassTestCondition, IClass>,
          IClassBinaryOperator
    {
        public ClassFilterCondition(IClassCollection condition, IFilter<IClass> filter, bool negate)
            : base(condition, filter, negate)
        {
            CeateAssertionFunc = (f, b) => new ClassRule(f, b);
        }
    }
}
