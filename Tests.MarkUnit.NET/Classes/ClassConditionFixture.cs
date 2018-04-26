using System;
using System.Collections.Generic;
using System.Linq;
using MarkUnit;
using MarkUnit.Assemblies;
using MarkUnit.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tests.MarkUnit.Classes
{
    [TestClass]
    public class ClassConditionFixture
    {
        [TestMethod]
        public void HasName_Should_FilterByPredicate()
        {
            var classFilter=new FilteredClasses(CreateSampleClasses());
            var sut = new ClassCondition(classFilter, false);
            
            sut.HasName(s => s == "A");

            Assert.AreEqual(1,classFilter.FilteredItems.Count());
            Assert.IsTrue(classFilter.FilteredItems.Any(f=>f.Name=="A"));
        }

        [TestMethod]
        public void HasNameMatching_Should_FilterByPattern()
        {
            var classFilter=new FilteredClasses(CreateSampleClasses());
            var sut = new ClassCondition(classFilter, false);
            
            sut.HasNameMatching("A");
            
            Assert.AreEqual(1,classFilter.FilteredItems.Count());
            Assert.IsTrue(classFilter.FilteredItems.Any(f=>f.Name=="A"));
        }

        [TestMethod]
        public void ImplementsInterface_Should_FilterByImplementedInterface()
        {
            var classFilter=new FilteredClasses(CreateSampleClasses());
            var sut = new ClassCondition(classFilter, false);
            _mockClass1.SetupGet(c => c.ClassType).Returns(typeof(SampleImplementation));
            _mockClass2.SetupGet(c => c.ClassType).Returns(typeof(int));
            _mockClass3.SetupGet(c => c.ClassType).Returns(typeof(int));

            sut.ImplementsInterface<ISample>();
            
            Assert.AreEqual(1,classFilter.FilteredItems.Count());
            Assert.IsTrue(classFilter.FilteredItems.Any(f=>f.Name=="A"));
        }

        [TestMethod]
        public void ImplementsInterfaceMatching_Should_FilterByImplementedInterfaceName()
        {
            var classFilter=new FilteredClasses(CreateSampleClasses());
            var sut = new ClassCondition(classFilter, false);
            _mockClass1.SetupGet(c => c.ClassType).Returns(typeof(SampleImplementation));
            _mockClass2.SetupGet(c => c.ClassType).Returns(typeof(int));
            _mockClass3.SetupGet(c => c.ClassType).Returns(typeof(int));

            sut.ImplementsInterfaceMatching("*Sampl*");
            
            Assert.AreEqual(1,classFilter.FilteredItems.Count());
            Assert.IsTrue(classFilter.FilteredItems.Any(f=>f.Name=="A"));
        }

        [TestMethod]
        public void Is_Should_FilterByTypePredicate()
        {
            var classFilter=new FilteredClasses(CreateSampleClasses());
            var sut = new ClassCondition(classFilter, false);
            _mockClass1.SetupGet(c => c.ClassType).Returns(typeof(SampleImplementation));
            _mockClass2.SetupGet(c => c.ClassType).Returns(typeof(int));
            _mockClass3.SetupGet(c => c.ClassType).Returns(typeof(int));

            sut.Is(t => t == typeof(SampleImplementation));
            
            Assert.AreEqual(1,classFilter.FilteredItems.Count());
            Assert.IsTrue(classFilter.FilteredItems.Any(f=>f.Name=="A"));
        }

        [TestMethod]
        public void HasAttribute_Should_FilterByAttribute()
        {
            var classFilter=new FilteredClasses(CreateSampleClasses());
            var sut = new ClassCondition(classFilter, false);
            _mockClass1.SetupGet(c => c.ClassType).Returns(typeof(ClassConditionFixture));
            _mockClass2.SetupGet(c => c.ClassType).Returns(typeof(int));
            _mockClass3.SetupGet(c => c.ClassType).Returns(typeof(int));

            sut.HasAttribute<TestClassAttribute>();
            
            Assert.AreEqual(1,classFilter.FilteredItems.Count());
            Assert.IsTrue(classFilter.FilteredItems.Any(f=>f.Name=="A"));
        }
        
        [TestMethod]
        public void IsDerivedFrom_Should_FilterByTypePredicate()
        {
            var classFilter=new FilteredClasses(CreateSampleClasses());
            var sut = new ClassCondition(classFilter, false);
            _mockClass1.SetupGet(c => c.ClassType).Returns(typeof(SampleDerived));
            _mockClass2.SetupGet(c => c.ClassType).Returns(typeof(int));
            _mockClass3.SetupGet(c => c.ClassType).Returns(typeof(int));

            sut.IsDerivedFrom<SampleImplementation>();
        
            Assert.AreEqual(1,classFilter.FilteredItems.Count());
            Assert.IsTrue(classFilter.FilteredItems.Any(f=>f.Name=="A"));
        }
        [TestMethod]
        public void IsDerivedFrom_Should_FilterByTypePredicate_WhenTypeIsGeneric()
        {
            var classFilter=new FilteredClasses(CreateSampleClasses());
            var sut = new ClassCondition(classFilter, false);
            _mockClass1.SetupGet(c => c.ClassType).Returns(typeof(ClassCondition));
            _mockClass2.SetupGet(c => c.ClassType).Returns(typeof(int));
            _mockClass3.SetupGet(c => c.ClassType).Returns(typeof(int));

            sut.IsDerivedFrom(typeof(TestCollectionBase<,,,>));
        
            Assert.AreEqual(1,classFilter.FilteredItems.Count());
            Assert.IsTrue(classFilter.FilteredItems.Any(f=>f.Name=="A"));
        }
        [TestMethod]
        public void IsDeclaredInAssembly_Should_FilterByAssemblyName()
        {
            var classFilter=new FilteredClasses(CreateSampleClasses());
            var sut = new ClassCondition(classFilter, false);
           
            var sampleAssembly = new MarkUnitAssembly(GetType().Assembly);
            var otherAssembly=new MarkUnitAssembly(typeof(List<>).Assembly);
            _mockClass1.SetupGet(c => c.Assembly).Returns(sampleAssembly);
            _mockClass2.SetupGet(c => c.Assembly).Returns(otherAssembly);
            _mockClass3.SetupGet(c => c.Assembly).Returns(otherAssembly);

            sut.IsDeclaredInAssembly(sampleAssembly.Name);
            
            Assert.AreEqual(1,classFilter.FilteredItems.Count());
            Assert.IsTrue(classFilter.FilteredItems.Any(f=>f.Name=="A"));
        } 
        
        [TestMethod]
        public void IsDeclaredInAssembly_Should_FilterByPredicate()
        {
            var classFilter=new FilteredClasses(CreateSampleClasses());
            var sut = new ClassCondition(classFilter, false);
           
            var sampleAssembly = new MarkUnitAssembly(GetType().Assembly);
            var otherAssembly=new MarkUnitAssembly(typeof(List<>).Assembly);
            _mockClass1.SetupGet(c => c.Assembly).Returns(sampleAssembly);
            _mockClass2.SetupGet(c => c.Assembly).Returns(otherAssembly);
            _mockClass3.SetupGet(c => c.Assembly).Returns(otherAssembly);

            sut.IsDeclaredInAssembly(a=>a==sampleAssembly.Assembly);
            
            Assert.AreEqual(1,classFilter.FilteredItems.Count());
            Assert.IsTrue(classFilter.FilteredItems.Any(f=>f.Name=="A"));
        }

        [TestMethod]
        public void IsDeclaredInAssemblyMatching_Should_FilterByPAttern()
        {
            var classFilter=new FilteredClasses(CreateSampleClasses());
            var sut = new ClassCondition(classFilter, false);
           
            var sampleAssembly = new MarkUnitAssembly(GetType().Assembly);
            var otherAssembly=new MarkUnitAssembly(typeof(List<>).Assembly);
            _mockClass1.SetupGet(c => c.Assembly).Returns(sampleAssembly);
            _mockClass2.SetupGet(c => c.Assembly).Returns(otherAssembly);
            _mockClass3.SetupGet(c => c.Assembly).Returns(otherAssembly);

            sut.IsDeclaredInAssemblyMatching("Tests.*");
            
            Assert.AreEqual(1,classFilter.FilteredItems.Count());
            Assert.IsTrue(classFilter.FilteredItems.Any(f=>f.Name=="A"));
        }

        private readonly Mock<IClassInfo> _mockClass1=new Mock<IClassInfo>();
        private readonly Mock<IClassInfo> _mockClass2=new Mock<IClassInfo>();
        private readonly Mock<IClassInfo> _mockClass3=new Mock<IClassInfo>();

        private IEnumerable<IClassInfo> CreateSampleClasses()
        {
            _mockClass1.SetupGet(c => c.Name).Returns("A");
            _mockClass2.SetupGet(c => c.Name).Returns("B");
            _mockClass3.SetupGet(c => c.Name).Returns("C");

            yield return _mockClass1.Object;
            yield return _mockClass2.Object;
            yield return _mockClass3.Object;
        }
 
        public interface ISample{}

        private class SampleImplementation : ISample
        {
        }

        private class SampleDerived : SampleImplementation{}
    }
}