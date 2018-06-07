namespace MarkUnit.Classes
{
    public interface IReducedClassCollection : IFilterConditionChain<IClassCollection, IClassTestCondition> { }

    public interface IReducedTypeCollection : IFilterConditionChain<ITypeCollection, ITypeTestCondition>{
        
    }
}
