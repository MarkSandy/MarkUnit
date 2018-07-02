namespace MarkUnit.Classes
{
    internal class ClassPredicateEx : IClassPredicateEx
    {
        private readonly ClassUsingClassCondition _classUsingClassCondition;
        
        public ClassPredicateEx(IAssertionVerifier<IClass> verifier)
        {
            _classUsingClassCondition = new ClassUsingClassCondition(verifier.Items, false);
        }

        public IPredicate<IClassUsesClassCondition> Except(params string[] exceptionPatterns)
        {
            // TODO: Add to xception list
            return this;
        }

        public IClassUsesClassCondition That()
        {
            return WhichOrThat("that");
        }

        private IClassUsesClassCondition WhichOrThat(string word)
        {
            PredicateString.Add(word);
            return _classUsingClassCondition;
        }

        public IClassUsesClassCondition Which()
        {
            return WhichOrThat("which");
        }
    }
}