namespace MarkUnit.Classes
{
    internal class TypeFilterCondition
        : FilterConditionBase<ITypeCollection, ITypeTestCondition, IType>,
            ITypeBinaryOperator
    {
        public TypeFilterCondition(ITypeCollection condition, IFilter<IType> filter, bool negate)
            : base(condition, filter, negate)
        {
            CeateAssertionFunc = (f, b) => new TypeRule(f, b);
        }
    }
}