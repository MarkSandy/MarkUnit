namespace MarkUnit.Classes
{
    internal class ClassPredicateEx : IClassPredicateEx
    {
        private string[] _exceptions;
        private readonly IAssertionVerifier<IClass> _verifier;

        public ClassPredicateEx(IAssertionVerifier<IClass> verifier)
        {
            _verifier = verifier;
        }

        public IClassUsesClassCondition That()
        {
            return WhichOrThat("that");
        }

        public IClassUsesClassCondition Which()
        {
            return WhichOrThat("which");
        }

        private IClassUsesClassCondition WhichOrThat(string word)
        {
            var classUsingClassCondition = new ClassUsingClassCondition(_verifier.Items, false);
            classUsingClassCondition.AddIgnoreList(_exceptions);
            var result = classUsingClassCondition;
            PredicateString.Add(word);
            return result;
        }
    }
}
