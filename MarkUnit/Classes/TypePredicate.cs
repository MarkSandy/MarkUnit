namespace MarkUnit.Classes
{
    internal class TypePredicate : ITypePredicate
    {
        private readonly ITypeCollectionFactory _typeCollectionFactory;
        private readonly ITypeCollector _typeCollector;
        private readonly bool _negate;
        private readonly bool _not;
        private string[] _exceptions=new string[0];

        public TypePredicate(ITypeCollector typeCollector, bool negate, bool not)
            : this(typeCollector,negate,not,new TypeCollectionFactory())
        {
        }

        public TypePredicate(ITypeCollector typeCollector, bool negate, bool not,ITypeCollectionFactory typeCollectionFactory)
        {
            _typeCollector = typeCollector;
            _negate = negate;
            _not = not;
            _typeCollectionFactory = typeCollectionFactory;
        }

        public IPredicate<ITypeCollection> Except(params string[] exceptionPatterns)
        {
            _exceptions = exceptionPatterns;
            return this;
        }

        public ITypeCollection That()
        {
            return ThatOrWhich("that");
        }

        private ITypeCollection ThatOrWhich(string word)
        {
            PredicateString.Add(word);
            return _typeCollectionFactory.Create(_typeCollector, _negate, _not,_exceptions);
        }

        public ITypeCollection Which()
        {
            return ThatOrWhich("which");
        }
    }
}