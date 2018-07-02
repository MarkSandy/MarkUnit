using System;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;

namespace MarkUnit.Classes
{
    internal class InterfacePredicate<T> : IInterfacePredicate where T : IClass
    {
        private readonly ClassMatchingInterfaceCondition _classMatchingInterfaceCondition;

        IFilter<IInterface> CreateMapper(IFilter<T> filter)
        {
            if (typeof(T)==typeof(IInterface)) return new InterfaceFilterMapper((IFilter<IInterface>)filter);
            if(typeof(T)==typeof(IClass)) return new ClassToInterfaceFilterMapper((IFilter<IClass>)filter);
            throw new InvalidOperationException($"invalid filter of type {typeof(T)}");
        }
        public InterfacePredicate(IAssertionVerifier<T> verifier)
        {
            _classMatchingInterfaceCondition = new ClassMatchingInterfaceCondition(CreateMapper(verifier.Items), false);
        }

        public IPredicate<IClassMatchingInterfaceCondition> Except(params string[] exceptionPatterns)
        {
            // TODO: Add to xception list
            return this;

        }

        public IClassMatchingInterfaceCondition That()
        {
            return WhichOrThat("that");
        }

        private IClassMatchingInterfaceCondition WhichOrThat(string word)
        {
            PredicateString.Add(word);
            return _classMatchingInterfaceCondition;
        }

        public IClassMatchingInterfaceCondition Which()
        {
            return WhichOrThat("which");
        }
    }
}