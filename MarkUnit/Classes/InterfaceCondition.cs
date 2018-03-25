namespace MarkUnit.Classes
{

    //internal class InterfaceLogicalLink : LogicalLink<IInterfaceTestCondition>, IInterfaceRule 
    //{
    //    private readonly IInternalInterfaceTestCondition _followUp;

    //    public InterfaceLogicalLink(IInternalInterfaceTestCondition followUp) : base(followUp)
    //    {
    //        _followUp = followUp;
    //    }

    //    public void Check()
    //    {
    //        _followUp.Check(); 
    //    }
    //}

    //internal class InterfaceRule : RuleBase<IClass,IInterfaceTestCondition,InterfaceRule>, IInternalInterfaceTestCondition
    //{
    //    public InterfaceRule(IFilter<IClass> items, bool negateAssertion) : base(items, negateAssertion)
    //    {
    //        LogicalLink = new InterfaceLogicalLink(this);

    //    }

    //    public ICheckableCondition<IInterfaceCondition> HasName(Expression<Predicate<string>> nameFilterExpression)
    //    {
    //        PredicateString.Add($"has name matching '{nameFilterExpression}'");
    //        var nameFilter = nameFilterExpression.Compile();
    //        return InnerHasName(nameFilter);
    //    }

    //    private ICheckableCondition<IInterfaceCondition> InnerHasName(Predicate<string> nameFilter)
    //    {
    //        return AppendCondition(c => c.ClassType.GetInterfaces().Any(i => nameFilter(i.Name)));
    //    }

    //    public ICheckableCondition<IInterfaceCondition> HasMatchingName(Expression<Func<Type, string>> matchingNameCreateExpression)
    //    {
    //        var createMatchingName = matchingNameCreateExpression.Compile();
    //        PredicateString.Add($"has a matching name '{matchingNameCreateExpression}'");
    //        return AppendCondition(c=>c.ClassType.GetInterfaces().Any(i=>i.Name.Matches(createMatchingName(c.ClassType))));
    //    }

    //    public ICheckableCondition<IInterfaceCondition> HasNameMatching(string pattern)
    //    {
    //        PredicateString.Add($"has name matching '{pattern}'");
    //        return InnerHasName(n => n.Matches(pattern));
    //    }

    //    public ICheckableCondition<IInterfaceCondition> ImplementsInterface<TInterface>()
    //    {
    //        return AppendCondition(c=>typeof(TInterface).IsAssignableFrom(c.ClassType));
    //    }

    //    private ICheckableCondition<IInterfaceCondition> AppendCondition(Predicate<IClass> classPredicate)
    //    {
    //        _verifier.AppendCondition(classPredicate);
    //        return _binaryOperator;
    //    }

    //    public ICheckableCondition<IInterfaceCondition> IsDeclaredInAssemblyMatching(string pattern)
    //    {
    //        return AppendCondition(c=>c.Assembly.Name.Matches(pattern));
    //    }

    //    public ICheckableCondition<IInterfaceCondition> IsDeclaredInAssembly(Expression<Predicate<Assembly>> assemblyFilterExpression)
    //    {
    //        PredicateString.Add($"is declared in assembly matching '{assemblyFilterExpression}'");
    //        var assemblyFilter = assemblyFilterExpression.Compile();
    //        return AppendCondition(c=>assemblyFilter(c.ClassType.Assembly));
    //    }

    //    public ICheckableCondition<IInterfaceCondition> Is(Expression<Predicate<Type>> typeFilterExpression)
    //    {
    //        PredicateString.Add($"is '{typeFilterExpression}'");
    //        var typeFilter = typeFilterExpression.Compile();
    //        return AppendCondition(c=>typeFilter(c.ClassType));
    //    }

    //    public void Period()
    //    {
    //      //  PredicateString.Passed(); // Falsch hier
    //    }

    //    public void Check()
    //    {
    //        _verifier.Verify();
    //    }
    //}
}