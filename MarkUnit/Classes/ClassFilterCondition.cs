﻿namespace MarkUnit.Classes
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

    internal class TypeFilterCondition
        : FilterConditionBase<ITypeCollection, ITypeTestCondition, IClass>,
            ITypeBinaryOperator
    {
        public TypeFilterCondition(ITypeCollection condition, IFilter<IClass> filter, bool negate)
            : base(condition, filter, negate)
        {
            CeateAssertionFunc = (f, b) => new TypeRule(f, b);
        }
    }


    
}
