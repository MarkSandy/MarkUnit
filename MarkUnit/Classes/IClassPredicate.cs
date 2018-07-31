namespace MarkUnit.Classes
{
    public interface IExceptions<out TCondition>
    {
        IPredicate<TCondition> Except(params string[] exceptionPatterns);
    }
    public interface IClassPredicate : IPredicate<IClassCollection>, IExceptions<IClassCollection>,IShould<IClassTestCondition> { }
}
