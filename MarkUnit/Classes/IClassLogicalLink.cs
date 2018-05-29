namespace MarkUnit.Classes
{
    public interface IClassRule
        : IRule<IClassTestCondition>,
          ICheckable { }


    public interface ITypeRule : IRule<ITypeTestCondition>, ICheckable
    {

    }
}
