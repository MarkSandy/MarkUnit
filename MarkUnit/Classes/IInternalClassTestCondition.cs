namespace MarkUnit.Classes
{
    internal interface IInternalClassTestCondition
        : IClassTestCondition,
          IInternalCheckable { }

    internal interface IInternalTypeTestCondition
        : ITypeTestCondition,
            IInternalCheckable { }

}

