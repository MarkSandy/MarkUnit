using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MarkUnit.Classes
{
  internal class ClassRule : RuleBase<IClass, IClassTestCondition,IClassRule>, IInternalClassTestCondition
    {
        public ClassRule(IAssertionVerifier<IClass> verifier) : base(verifier)
        {
            LogicalLink = new ClassLogicalLink(this);
        }

        public ClassRule(IFilter<IClass> items, bool negateAssertion) : base(items, negateAssertion)
        {
            LogicalLink = new ClassLogicalLink(this);
        }
         
        public IClassRule HaveNameMatching(string pattern)
        {
            PredicateString.Add($"have a name matching '{pattern}'");
            return AppendCondition(s => s.Name.Matches(pattern));
        }

        public IClassRule HaveName(Expression<Predicate<string>> nameFilterExpression)
        {
            PredicateString.Add($"have a name matching '{nameFilterExpression}'");
            var nameFilter = nameFilterExpression.Compile();
            return AppendCondition(c => nameFilter(c.Name));
        }

        public IClassRule ReferenceNamespacesMatching(string pattern)
        {
            PredicateString.Add($"reference a namespace matching '{pattern}'");
            return AppendCondition(c => c.ReferencedNameSpaces.Any(n => n.Matches(pattern)));
        }

        public IClassRule ImplementInterfaceMatching(string pattern)
        {
            PredicateString.Add($"implement an interface matching '{pattern}'");
            return AppendCondition(c => c.ClassType.GetInterfaces().Any(i => i.Name.Matches(pattern)));
        }

        public IClassRule ImplementInterface<T>()
        {
            PredicateString.Add($"implement interface {typeof(T).Name}");
            return AppendCondition(c => typeof(T).IsAssignableFrom(c.ClassType));
        }

        public IInterfacePredicate ImplementInterface()
        {
            PredicateString.Add($"implement interface");
            return new InterfacePredicate(Verifier);
        }

        public IClassRule UsesClassMatching(string regExOnClassName, string regExOnMatchingClass)
        {
            PredicateString.Add($"uses a class matching the regex expressions('{regExOnClassName}','{regExOnMatchingClass}'");
            return AppendCondition(c => c.ReferencedClasses.Any(x => MatchesName(x.Name, regExOnClassName, regExOnMatchingClass)));
        }

        public IClassRule BeInAssemblyMatching(string pattern)
        {
            PredicateString.Add($"be in an assembly matching '{pattern}'");
            return AppendCondition(c => c.Assembly.Name.Matches(pattern));
        }

        public IClassRule BeInAssembly(Assembly assembly)
        {
            PredicateString.Add($"be in assembly '{assembly.FullName}'");
            return AppendCondition(c => c.Assembly.Assembly.FullName == assembly.FullName);
        }

        public IClassRule BeDeclaredInNamespaceMatching(string pattern)
        {
            PredicateString.Add($"be declared in namespace matching '{pattern}'");
            return AppendCondition(c => c.Namespace.Matches(pattern));
        }

        public IClassRule Be(Expression<Predicate<Type>> typeFilterExpression)
        {
            PredicateString.Add($"be {typeFilterExpression}");
            var typePredicate = typeFilterExpression.Compile();
            return AppendCondition(c => typePredicate(c.ClassType));
        }

        bool MatchesName(string name, string regExOnClassName, string regExOnMatchingClass)
        {
            string repl = Regex.Replace(name, regExOnClassName, regExOnMatchingClass);
            return name.Matches(repl);
        }
    }
    internal class TypeRule : RuleBase<IClass, ITypeTestCondition,ITypeRule>, IInternalTypeTestCondition
    {
        public TypeRule(IAssertionVerifier<IClass> verifier) : base(verifier)
        {
            LogicalLink = new TypeLogicalLink(this);
        }

        public TypeRule(IFilter<IClass> items, bool negateAssertion) : base(items, negateAssertion)
        {
            LogicalLink = new TypeLogicalLink(this);
        }
         
        public ITypeRule HaveNameMatching(string pattern)
        {
            PredicateString.Add($"have a name matching '{pattern}'");
            return AppendCondition(s => s.Name.Matches(pattern));
        }

        public ITypeRule HaveName(Expression<Predicate<string>> nameFilterExpression)
        {
            PredicateString.Add($"have a name matching '{nameFilterExpression}'");
            var nameFilter = nameFilterExpression.Compile();
            return AppendCondition(c => nameFilter(c.Name));
        }

        public ITypeRule ReferenceNamespacesMatching(string pattern)
        {
            PredicateString.Add($"reference a namespace matching '{pattern}'");
            return AppendCondition(c => c.ReferencedNameSpaces.Any(n => n.Matches(pattern)));
        }

        public IInterfacePredicate ImplementInterface()
        {
            throw new NotImplementedException();
        }

        public ITypeRule ImplementInterfaceMatching(string pattern)
        {
            PredicateString.Add($"implement an interface matching '{pattern}'");
            return AppendCondition(c => c.ClassType.GetInterfaces().Any(i => i.Name.Matches(pattern)));
        }

        public ITypeRule ImplementInterface<T>()
        {
            PredicateString.Add($"implement interface {typeof(T).Name}");
            return AppendCondition(c => typeof(T).IsAssignableFrom(c.ClassType));
        }
 
        public ITypeRule UsesClassMatching(string regExOnClassName, string regExOnMatchingClass)
        {
            PredicateString.Add($"uses a class matching the regex expressions('{regExOnClassName}','{regExOnMatchingClass}'");
            return AppendCondition(c => c.ReferencedClasses.Any(x => MatchesName(x.Name, regExOnClassName, regExOnMatchingClass)));
        }

        public ITypeRule BeInAssemblyMatching(string pattern)
        {
            PredicateString.Add($"be in an assembly matching '{pattern}'");
            return AppendCondition(c => c.Assembly.Name.Matches(pattern));
        }

        public ITypeRule BeInAssembly(Assembly assembly)
        {
            PredicateString.Add($"be in assembly '{assembly.FullName}'");
            return AppendCondition(c => c.Assembly.Assembly.FullName == assembly.FullName);
        }

        public ITypeRule BeDeclaredInNamespaceMatching(string pattern)
        {
            PredicateString.Add($"be declared in namespace matching '{pattern}'");
            return AppendCondition(c => c.Namespace.Matches(pattern));
        }

        public ITypeRule Be(Expression<Predicate<Type>> typeFilterExpression)
        {
            PredicateString.Add($"be {typeFilterExpression}");
            var typePredicate = typeFilterExpression.Compile();
            return AppendCondition(c => typePredicate(c.ClassType));
        }

        bool MatchesName(string name, string regExOnClassName, string regExOnMatchingClass)
        {
            string repl = Regex.Replace(name, regExOnClassName, regExOnMatchingClass);
            return name.Matches(repl);
        }
    }
}