namespace MarkUnit.Classes
{
    public interface IClassUsingClassRule
        : IRule<IClassUsesClassCondition>,
          ICheckable { }
}
