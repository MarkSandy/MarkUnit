namespace MarkUnit.Classes
{
    internal class ClassPredicate : IClassPredicate
    {
        private readonly IClassCollector _classCollector;
        private readonly bool _negate;

        public ClassPredicate(IClassCollector classCollector, bool negate)
        {
            _classCollector = classCollector;
            _negate = negate;
        }

        public IClassCollection That()
        {
            PredicateString.Add("that");
            var classes = _classCollector.Get();
            var classFilter = new FilteredClasses(classes);
            return new ClassCondition(classFilter, _negate);
        }
    }
}
