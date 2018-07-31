using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MarkUnit.Classes
{
    internal class TypeConditionBase<TType,TCollection,TTest,TReduced> 
        :TestCollectionBase<TType, TCollection, TTest, TReduced> 
        where TType : IType,INamedComponent 
        where TReduced : IFilterConditionChain<TCollection, TTest> 

    {
        public TypeConditionBase(IFilter<TType> items) : base(items)
        {
        }
        
        public TReduced IsDeclaredInAssemblyMatching(string pattern)
        {
            PredicateString.Add($"is declared in an assembly matching '{pattern}'");
            return AppendCondition(c => c.Assembly.Name.Matches(pattern));
        }

        public TReduced IsDeclaredInAssembly(Assembly assembly)
        {
            PredicateString.Add($"is declared in assembly '{assembly.FullName}'");
            return AppendCondition(c => c.Assembly.Assembly.FullName==assembly.FullName);
        }

        public TReduced IsDeclaredInAssembly(string name)
        {
            PredicateString.Add($"is declared in an assembly '{name}'");
            return AppendCondition(c => c.Assembly.Name == name);
        }

        public TReduced IsDeclaredInAssembly(Expression<Predicate<Assembly>> assemblyFilterExpression)
        {
            var predicate = assemblyFilterExpression.Compile();
            PredicateString.Add($"is declared in an assembly matching '{assemblyFilterExpression}'");
            return AppendCondition(c => predicate(c.Assembly.Assembly));
        }

        public TReduced Is(Expression<Predicate<Type>> typeExpression)
        {
            PredicateString.Add($"Is {typeExpression}");
            var predicate = typeExpression.Compile();
            return AppendCondition(c =>  predicate(c.ClassType));
        }

        public TReduced HasAttribute<TAttribute>() where TAttribute : Attribute
        {
            PredicateString.Add($"has attribute '{typeof(TAttribute).Name}'");
            return AppendCondition(c => ClassHasAttribute(c.ClassType, typeof(TAttribute)));
        }

        private bool ClassHasAttribute(Type classType, Type attributeType)
        {
            var ad = classType.GetCustomAttributesData();
            var result = ad.Any(a => a.AttributeType.ToString() == attributeType.ToString());
            return result;
        }
    }
}