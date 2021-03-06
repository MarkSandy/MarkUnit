﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarkUnit.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters;
using MarkUnit.Assemblies;
using Moq;

namespace Tests.MarkUnit.Classes
{
    [TestClass()]
    public class ClassCollectorFixture
    {

        [TestMethod()]
        public void Get_Should_ExamineAllClassesFromAssemblies()
        {
            var assemblyFilter=new Mock<IFilteredAssemblies>().Object; 
            var classReaderMock = new Mock<IClassReader>();
            var c1 = new Mock<IClassInfo>().Object;
            var c2 = new Mock<IClassInfo>().Object;
            var classes=new IClassInfo[]{c1,c2};
            classReaderMock.Setup(c => c.LoadFromAssemblies(assemblyFilter)).Returns(classes);
            var classInfoCollectorMock = new Mock<IClassInfoCollector>();
            var examinedClasses = new List<IClassInfo>();
            classInfoCollectorMock.Setup(c => c.Examine(It.IsAny<IClassInfo>())).Callback<IClassInfo>(c => examinedClasses.Add(c));
            

            var sut=new ClassCollector(classReaderMock.Object,classInfoCollectorMock.Object);
            sut.Assemblies=assemblyFilter;

            var actualClasses=sut.Get().ToArray();
            Assert.IsTrue(examinedClasses.Count==2);
            Assert.IsTrue(examinedClasses.Contains(c1));
            Assert.IsTrue(examinedClasses.Contains(c2));

            Assert.IsTrue(actualClasses.Length==2);
            Assert.IsTrue(actualClasses.Contains(c1));
            Assert.IsTrue(actualClasses.Contains(c2));

        }
    }
}