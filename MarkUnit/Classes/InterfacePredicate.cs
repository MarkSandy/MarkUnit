using System;

namespace MarkUnit.Classes
{
    internal class InterfacePredicate<T> : IInterfacePredicate
        where T : IClass
    {
        private string[] _exceptions;
        private readonly IAssertionVerifier<T> _verifier;

        public InterfacePredicate(IAssertionVerifier<T> verifier)
        {
            _verifier = verifier;
        }

        public IClassMatchingInterfaceCondition That()
        {
            return WhichOrThat("that");
        }

        public IClassMatchingInterfaceCondition Which()
        {
            return WhichOrThat("which");
        }

        public IPredicate<IClassMatchingInterfaceCondition> Except(params string[] exceptionPatterns)
        {
            _exceptions = exceptionPatterns;
            return this;
        }

        IFilter<IInterface> CreateMapper(IFilter<T> filter)
        {
            if (typeof(T) == typeof(IInterface)) return new InterfaceFilterMapper((IFilter<IInterface>) filter);
            if (typeof(T) == typeof(IClass)) return new ClassToInterfaceFilterMapper((IFilter<IClass>) filter);
            throw new InvalidOperationException($"invalid filter of type {typeof(T)}");
        }

        private IClassMatchingInterfaceCondition WhichOrThat(string word)
        {
            PredicateString.Add(word);
            var classMatchingInterfaceCondition = new ClassMatchingInterfaceCondition(CreateMapper(_verifier.Items), false);

            return classMatchingInterfaceCondition;
        }
    }
}
