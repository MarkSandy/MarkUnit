﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MarkUnit.Classes
{
    internal class ClassRule : RuleBase<IClassInfo, IClassTestCondition,IClassRule>, IInternalClassTestCondition
    {
        public ClassRule(IAssertionVerifier<IClassInfo> verifier) : base(verifier)
        {
            LogicalLink = new ClassLogicalLink(this);
        }

        public ClassRule(IFilter<IClassInfo> items, bool negateAssertion) : base(items, negateAssertion)
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
            return AppendCondition(c => c.ClassType.ImplementsInterface<T>());
        }

        public IInterfacePredicate ImplementInterface()
        {
            PredicateString.Add($"implement interface");
            return new InterfacePredicate(Verifier);
        }

        public IClassRule UsesClassMatching(string regExOnClassName, string regExOnMatchingClass)
        {
            PredicateString.Add($"uses a class matching the regex expressions('{regExOnClassName}','{regExOnMatchingClass}'");
            return AppendCondition(c => c.ReferencedClasses.Any(x => x.Name.MatchesRegEx(x.Name,regExOnClassName, regExOnMatchingClass)));
        }

        public IClassRule BeInAssembly(Expression<Predicate<Assembly>> assemblyFilterExpression)
        {
            PredicateString.Add($"be in an assembly matching '{assemblyFilterExpression}'");
            var filter = assemblyFilterExpression.Compile();
            return AppendCondition(c => filter(c.AssemblyInfo.Assembly.Assembly));
        }

        public IClassRule BeInAssemblyMatching(string pattern)
        {
            PredicateString.Add($"be in an assembly matching '{pattern}'");
            return AppendCondition(c => c.AssemblyInfo.Name.Matches(pattern));
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
 
    }
}