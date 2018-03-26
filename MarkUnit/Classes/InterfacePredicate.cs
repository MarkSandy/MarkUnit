namespace MarkUnit.Classes
{
    internal class InterfacePredicate : IInterfacePredicate
    {
        private readonly IAssertionVerifier<IClass> _verifier;

        public InterfacePredicate(IAssertionVerifier<IClass> verifier)
        {
            _verifier = verifier;
        }

        public IClassMatchingInterfaceCondition That()
        {
            PredicateString.Add("that");
            return  new ClassMatchingInterfaceCondition(new ClassToInterfaceFilterMapper(_verifier.Items),false);
        }
    }
}